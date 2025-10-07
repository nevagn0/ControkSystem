using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Domain.Model;

namespace ControkSystem.Application.Services;
using ControkSystem.Application.DTOs;
public class ProjectServices
{
    private readonly IProjectrepository _projectRepository;
    public ProjectServices(IProjectrepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto)
    {
        var newProject = new Projects
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            Progres = dto.Progres,
        };
        await _projectRepository.AddAsync(newProject);

        return new ProjectDto
        {
            Id = newProject.Id,
            Name = newProject.Name,
            CreatedAt = newProject.CreatedAt,
            Progres = newProject.Progres
        };
    }

    public async Task<ProjectDto> GetByIdASync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CreatedAt = project.CreatedAt,
            Progres = project.Progres
        };
    }

    public async Task<bool> DeleteProjectAsync(Guid id)
    {
        var defect = await _projectRepository.ExistsAsync(id);
        if (!defect)
            return false;
        await _projectRepository.DeleteAsync(id);
        return true;
    }

    public async Task<ProjectDto> UpdateASync(Guid id, Updateproject dto)
    {
        var UpProg = await _projectRepository.GetByIdAsync(id);
        
        UpProg.Name = dto.Name;
        UpProg.Progres = dto.Progres;
        await _projectRepository.UpdateAsync(UpProg);

        return new ProjectDto
        {
            Id = UpProg.Id,
            Name = UpProg.Name,
            CreatedAt = UpProg.CreatedAt,
            Progres = UpProg.Progres
        };
        
    }
}