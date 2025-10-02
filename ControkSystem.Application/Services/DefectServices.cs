namespace ControkSystem.Application.Services;
using ControkSystem.Domain.Model;
using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Application.DTOs;
public class DefectServices
{
    private readonly IDefectRepository _defectRepository;

    public DefectServices(IDefectRepository defectRepository)
    {
        _defectRepository = defectRepository;
    }

    public async Task<DefectDto> CreateDefect(CreateDefectDto dto)
    {
        var defect = new Defects
        {
            Name = dto.Name,
            Title = dto.Title,
            Priority = dto.Priority,
            DeadLine = dto.DeadLine,
            Pictures = dto.Pictures,
            Status = dto.Status,
            Comm = dto.Comm,
            IdProject = dto.IdProject,
            UserId = dto.UserId,
        };
        await _defectRepository.AddAsync(defect);

        return new DefectDto
        {
            Id = defect.Id,
            Name = defect.Name,
            Title = defect.Title,
            Priority = defect.Priority,
            DeadLine = defect.DeadLine,
            Pictures = defect.Pictures,
            Status = defect.Status,
            Comm = defect.Comm,
            IdProject = defect.IdProject,
            UserId = defect.UserId,
        };
    }

    public async Task<DefectDto> GetDefectById(Guid id)
    {
        var defect = await _defectRepository.GetDefectById(id);
        return new DefectDto
        {
            Id = defect.Id,
            Name = defect.Name,
            Title = defect.Title,
            Priority = defect.Priority,
            DeadLine = defect.DeadLine,
            Pictures = defect.Pictures,
            Status = defect.Status,
            Comm = defect.Comm
        };
    }

    public async Task<bool> DeleteDefectAsync(Guid id)
    {
        var defect = await _defectRepository.ExistsDefectAsync(id);
        if (!defect)
            return false;
        await _defectRepository.DeleteAsync(id);
        return true;
    }
}