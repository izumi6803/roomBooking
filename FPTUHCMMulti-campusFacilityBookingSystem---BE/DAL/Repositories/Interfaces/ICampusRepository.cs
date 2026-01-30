using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface ICampusRepository
    {
        Task<Campus?> GetByIdAsync(string id);
        Task<(List<Campus> items, int total)> GetPagedAsync(int page, int limit);
        Task<int> CreateAsync(Campus entity);
        Task<int> UpdateAsync(Campus entity);
        Task<List<Campus>> GetAllAsync();
        Task<List<Campus>> GetAllCampusesAsync();
    }
}


