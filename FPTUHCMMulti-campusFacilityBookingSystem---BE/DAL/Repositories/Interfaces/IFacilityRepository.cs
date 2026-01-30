using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IFacilityRepository
    {
        Task<Facility?> GetByIdAsync(string id);
        Task<Facility?> GetByIdWithDetailsAsync(string id);
        Task<(List<Facility> items, int total)> GetFilteredAsync(string? name, string? status, string? typeId, string? campusId, int page, int limit);
        Task<List<Facility>> GetFacilitiesWithAvailabilityAsync(string campusId, DateTime from, DateTime to);
        Task<int> CreateAsync(Facility entity);
        Task<int> UpdateAsync(Facility entity);
        Task<List<Facility>> GetAllAsync();
    }
}


