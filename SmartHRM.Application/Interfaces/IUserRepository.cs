using SmartHRM.Core.Entities;

namespace SmartHRM.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
