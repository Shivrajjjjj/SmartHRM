using SmartHRM.Core.Entities;

namespace SmartHRM.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();          // Get all roles
        Task<Role?> GetByIdAsync(int id);               // Get role by Id
        Task<Role> AddAsync(Role role);                // Add a new role
        Task<Role> UpdateAsync(Role role);             // Update an existing role
        Task<bool> DeleteAsync(int id);                // Delete a role by Id
    }
}

