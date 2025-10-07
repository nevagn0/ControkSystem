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
            ProjectId = dto.ProjectId,
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
            ProjectId = defect.ProjectId,
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
            Comm = defect.Comm,
            ProjectId = defect.ProjectId,
            UserId = defect.UserId,
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

    public async Task<DefectDto> UpdateDefect(Guid id, UpdateDefectDto dto)
    {
        var UpDef = await _defectRepository.GetDefectById(id);
        if (UpDef == null)
        {
            throw new ArgumentException($"Дефект {id} не найден");
        }
        UpDef.Status = dto.Status;
        UpDef.Comm = dto.Comm;
        await _defectRepository.UpdateDefect(UpDef);
        return new DefectDto
        {
            Id = UpDef.Id,
            Name = UpDef.Name,
            Title = UpDef.Title,
            Priority = UpDef.Priority,
            DeadLine = UpDef.DeadLine,
            Pictures = UpDef.Pictures,
            Status = UpDef.Status,
            Comm = UpDef.Comm,
            ProjectId = UpDef.ProjectId,
            UserId = UpDef.UserId,
        };
        
    }
}