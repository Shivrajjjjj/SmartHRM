using Microsoft.EntityFrameworkCore;
using SmartHRM.Application.Interfaces;
using SmartHRM.Core.Entities;
using SmartHRM.Infrastructure.Data;

namespace SmartHRM.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SmartHRMDbContext _context;

    public UserRepository(SmartHRMDbContext context)
    {
        _context = context;
    }

    // Add a new user
    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Get all users with role info
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
                             .Include(u => u.Role)
                             .ToListAsync();
    }

    // Get user by ID
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
                             .Include(u => u.Role)
                             .FirstOrDefaultAsync(u => u.UserId == id); // ✅ Changed Id → UserId
    }

    // Update existing user
    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Delete user by ID
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id); // ✅ FindAsync works with UserId
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
