using ControkSystem.Domain.Model;

namespace ControkSystem.Domain.Interfaces.Repositories;

public interface IUserProjectRepository
{
    Task<UserProject> CreateUserProjectAsync(Guid userId, Guid projectId);
    Task DeleteUserProjectAsync(Guid userId, Guid projectId);
    Task<bool> ExistsAsync(Guid userId, Guid projectId);
    Task<UserProject?> GetByIdAsync(Guid userId, Guid projectId);
    Task<IEnumerable<UserProject>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserProject>> GetByProjectIdAsync(Guid projectId);
}