namespace ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Domain.Model;
public interface IDefectRepository
{
    Task AddAsync(Defects defects);
    Task <Defects?> GetDefectById(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateDefect(Defects defects);
    Task<bool> ExistsDefectAsync(Guid id);
    
}