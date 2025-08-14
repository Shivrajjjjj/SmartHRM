using SmartHRM.Core.Entities;

namespace SmartHRM.Application.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization> AddAsync(Organization organization);
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization?> GetByIdAsync(int id);
        Task<Organization> UpdateAsync(Organization organization);
        Task<bool> DeleteAsync(int id);
    }
}
