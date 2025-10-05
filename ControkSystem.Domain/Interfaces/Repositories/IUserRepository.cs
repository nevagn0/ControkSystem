using ControkSystem.Domain.Model;

namespace ControkSystem.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByLoginAsync(string login);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}