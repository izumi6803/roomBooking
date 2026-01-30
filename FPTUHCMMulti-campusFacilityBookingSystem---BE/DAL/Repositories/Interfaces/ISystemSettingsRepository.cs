using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface ISystemSettingsRepository
    {
        Task<SystemSettings?> GetByKeyAsync(string key);
        Task<List<SystemSettings>> GetAllAsync();
        Task<int> CreateAsync(SystemSettings entity);
        Task<int> UpdateAsync(SystemSettings entity);
        Task<int> CreateOrUpdateAsync(string key, string value, string? description = null);
    }
}

