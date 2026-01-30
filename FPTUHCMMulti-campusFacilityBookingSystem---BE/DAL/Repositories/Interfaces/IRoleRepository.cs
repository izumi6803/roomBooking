using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(string id);
        Task<(List<Role> items, int total)> GetPagedAsync(int page, int limit);
        Task<int> CreateAsync(Role entity);
        Task<int> UpdateAsync(Role entity);
        Task<List<Role>> GetAllAsync();
    }
}


