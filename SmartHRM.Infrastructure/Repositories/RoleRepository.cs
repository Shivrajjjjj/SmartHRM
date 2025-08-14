using Microsoft.EntityFrameworkCore;
using SmartHRM.Application.Interfaces;
using SmartHRM.Core.Entities;
using SmartHRM.Infrastructure.Data;

namespace SmartHRM.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly SmartHRMDbContext _context;

    public RoleRepository(SmartHRMDbContext context)
    {
        _context = context;
    }

    public async Task<Role> AddAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }
}
