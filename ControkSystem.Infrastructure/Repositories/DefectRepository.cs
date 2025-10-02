using ControkSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControkSystem.Infrastructure.Repositories;
using ControkSystem.Domain.Model;
using ControkSystem.Domain.Interfaces.Repositories;
public class DefectRepository : IDefectRepository
{
    private readonly ControlSystemDbContext _context;

    public DefectRepository(ControlSystemDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Defects defects)
    {
        await _context.Defects.AddAsync(defects);
        await _context.SaveChangesAsync();
    }

    public async Task<Defects?> GetDefectById(Guid id)
    {
        return await _context.Defects.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var defects = await GetDefectById(id);
        if (defects != null)
        {
            _context.Defects.Remove(defects);
            _context.SaveChangesAsync();
        }
    }

    public async Task UpdateDefect(Defects defects)
    {
        _context.Defects.Update(defects);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsDefectAsync(Guid id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }
}