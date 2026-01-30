using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(string id);
        Task<(List<Booking> items, int total)> GetPagedAsync(string? userId, string? facilityId, string? status, int page, int limit);
        Task<int> CreateAsync(Booking entity);
        Task<int> UpdateAsync(Booking entity);
        Task<List<Booking>> GetAllAsync();
        Task<bool> HasConflictAsync(string facilityId, DateTime startTime, DateTime endTime, string? excludeBookingId = null);
    }
}

