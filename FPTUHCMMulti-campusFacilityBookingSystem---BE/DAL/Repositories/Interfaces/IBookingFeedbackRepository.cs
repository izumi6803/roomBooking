using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IBookingFeedbackRepository
    {
        Task<BookingFeedback?> GetByIdAsync(string id);
        Task<BookingFeedback?> GetByIdWithDetailsAsync(string id);
        Task<List<BookingFeedback>> GetAllAsync();
        Task<List<BookingFeedback>> GetByBookingIdAsync(string bookingId);
        Task<List<BookingFeedback>> GetByUserIdAsync(string userId);
        Task<(List<BookingFeedback> items, int total)> GetFilteredAsync(
            string? userId, 
            string? bookingId, 
            int? minRating, 
            int? maxRating,
            bool? reportIssue,
            bool? isResolved,
            int page, 
            int limit);
        Task<BookingFeedback?> GetByBookingIdSingleAsync(string bookingId);
        Task<bool> BookingHasFeedbackAsync(string bookingId);
        Task<double> GetAverageFacilityRatingAsync(string facilityId);
        Task<List<BookingFeedback>> GetUnresolvedIssuesAsync();
        Task AddAsync(BookingFeedback feedback);
        Task UpdateAsync(BookingFeedback feedback);
        Task DeleteAsync(BookingFeedback feedback);
        Task<int> SaveChangesAsync();
    }
}

