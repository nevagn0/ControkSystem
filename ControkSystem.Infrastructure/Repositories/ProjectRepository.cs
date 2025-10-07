using ControkSystem.Domain.Model;
using ControkSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControkSystem.Infrastructure.Repositories;
using ControkSystem.Domain.Interfaces.Repositories;
public class ProjectRepository : IProjectrepository
{
    private readonly ControlSystemDbContext  _context;
    public ProjectRepository(ControlSystemDbContext context)
    {
        _context = context;
    }

    public async Task<Projects?> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<IEnumerable<Projects>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task AddAsync(Projects project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task? UpdateASync(Projects project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Projects project)
    { 
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await GetByIdAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Projects.AnyAsync(p => p.Id == id);
    }
}