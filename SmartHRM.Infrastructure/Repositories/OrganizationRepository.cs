using Microsoft.EntityFrameworkCore;
using SmartHRM.Application.Interfaces;
using SmartHRM.Core.Entities;
using SmartHRM.Infrastructure.Data;

namespace SmartHRM.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly SmartHRMDbContext _context;

    public OrganizationRepository(SmartHRMDbContext context)
    {
        _context = context;
    }

    public async Task<Organization> AddAsync(Organization org)
    {
        _context.Organizations.Add(org);
        await _context.SaveChangesAsync();
        return org;
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organization?> GetByIdAsync(int id)
    {
        return await _context.Organizations.FindAsync(id);
    }

    public async Task<Organization> UpdateAsync(Organization org)
    {
        _context.Organizations.Update(org);
        await _context.SaveChangesAsync();
        return org;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var org = await _context.Organizations.FindAsync(id);
        if (org == null) return false;

        _context.Organizations.Remove(org);
        await _context.SaveChangesAsync();
        return true;
    }
}
