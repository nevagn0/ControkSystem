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
}