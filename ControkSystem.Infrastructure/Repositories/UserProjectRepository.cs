using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Domain.Model;
using ControkSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControkSystem.Infrastructure.Repositories;
using ControkSystem.Domain.Interfaces;
public class UserProjectRepository : IUserProjectRepository
{
    private readonly ControlSystemDbContext _context;

    public UserProjectRepository(ControlSystemDbContext context)
    {
        _context = context;
    }

    public async Task<UserProject> CreateUserProjectAsync(Guid userId, Guid projectId)
    {
        var userproject = new UserProject
        {
            IdUser = userId,
            IdProject = projectId,
        };
        await _context.UserProjects.AddAsync(userproject);
        await _context.SaveChangesAsync();
        return userproject;
    } public async Task DeleteUserProjectAsync(Guid userId, Guid projectId)
    {
        var userProject = await GetByIdAsync(userId, projectId);
        if (userProject != null)
        {
            _context.UserProjects.Remove(userProject);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid projectId)
    {
        return await _context.UserProjects
            .AnyAsync(up => up.IdUser == userId && up.IdProject == projectId);
    }

    public async Task<UserProject?> GetByIdAsync(Guid userId, Guid projectId)
    {
        return await _context.UserProjects
            .FirstOrDefaultAsync(up => up.IdUser == userId && up.IdProject == projectId);
    }

    public async Task<IEnumerable<UserProject>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserProjects
            .Where(up => up.IdUser == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserProject>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.UserProjects
            .Where(up => up.IdProject == projectId)
            .ToListAsync();
    }
    
}