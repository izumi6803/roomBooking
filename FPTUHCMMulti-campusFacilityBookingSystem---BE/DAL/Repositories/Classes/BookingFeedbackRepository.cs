using DAL.Dbcontext;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class BookingFeedbackRepository : IBookingFeedbackRepository
    {
        private readonly FacilityBookingDbContext _context;

        public BookingFeedbackRepository(FacilityBookingDbContext context)
        {
            _context = context;
        }

        public async Task<BookingFeedback?> GetByIdAsync(string id)
        {
            return await _context.BookingFeedbacks
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

        public async Task<BookingFeedback?> GetByIdWithDetailsAsync(string id)
        {
            return await _context.BookingFeedbacks
                .Include(f => f.User)
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

        public async Task<List<BookingFeedback>> GetAllAsync()
        {
            return await _context.BookingFeedbacks
                .Include(f => f.User)
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BookingFeedback>> GetByBookingIdAsync(string bookingId)
        {
            return await _context.BookingFeedbacks
                .Include(f => f.User)
                .Where(f => f.BookingId == bookingId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BookingFeedback>> GetByUserIdAsync(string userId)
        {
            return await _context.BookingFeedbacks
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<(List<BookingFeedback> items, int total)> GetFilteredAsync(
            string? userId,
            string? bookingId,
            int? minRating,
            int? maxRating,
            bool? reportIssue,
            bool? isResolved,
            int page,
            int limit)
        {
            var query = _context.BookingFeedbacks
                .Include(f => f.User)
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(f => f.UserId == userId);

            if (!string.IsNullOrEmpty(bookingId))
                query = query.Where(f => f.BookingId == bookingId);

            if (minRating.HasValue)
                query = query.Where(f => f.Rating >= minRating.Value);

            if (maxRating.HasValue)
                query = query.Where(f => f.Rating <= maxRating.Value);

            if (reportIssue.HasValue)
                query = query.Where(f => f.ReportIssue == reportIssue.Value);

            if (isResolved.HasValue)
            {
                if (isResolved.Value)
                    query = query.Where(f => f.ResolvedAt != null);
                else
                    query = query.Where(f => f.ResolvedAt == null && f.ReportIssue);
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(f => f.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<BookingFeedback?> GetByBookingIdSingleAsync(string bookingId)
        {
            return await _context.BookingFeedbacks
                .Include(f => f.User)
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .FirstOrDefaultAsync(f => f.BookingId == bookingId);
        }

        public async Task<bool> BookingHasFeedbackAsync(string bookingId)
        {
            return await _context.BookingFeedbacks
                .AnyAsync(f => f.BookingId == bookingId);
        }

        public async Task<double> GetAverageFacilityRatingAsync(string facilityId)
        {
            var ratings = await _context.BookingFeedbacks
                .Include(f => f.Booking)
                .Where(f => f.Booking.FacilityId == facilityId)
                .Select(f => f.Rating)
                .ToListAsync();

            return ratings.Any() ? ratings.Average() : 0.0;
        }

        public async Task<List<BookingFeedback>> GetUnresolvedIssuesAsync()
        {
            return await _context.BookingFeedbacks
                .Include(f => f.User)
                .Include(f => f.Booking)
                    .ThenInclude(b => b.Facility)
                .Where(f => f.ReportIssue && f.ResolvedAt == null)
                .OrderBy(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(BookingFeedback feedback)
        {
            await _context.BookingFeedbacks.AddAsync(feedback);
        }

        public async Task UpdateAsync(BookingFeedback feedback)
        {
            _context.BookingFeedbacks.Update(feedback);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(BookingFeedback feedback)
        {
            _context.BookingFeedbacks.Remove(feedback);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

