namespace ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Domain.Model;
public interface IProjectrepository
{
    Task<Projects?> GetByIdAsync(Guid id);
    Task<IEnumerable<Projects>> GetAllAsync();
    Task AddAsync(Projects project);
    Task UpdateAsync(Projects project);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}