using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<Booking> items, int total)> GetPagedAsync(string? userId, string? facilityId, string? status, int page, int limit)
        {
            var query = _context.Set<Booking>()
                .Include(b => b.User)
                .Include(b => b.Facility)
                .Include(b => b.Feedback)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(b => b.UserId == userId);
            }

            if (!string.IsNullOrEmpty(facilityId))
            {
                query = query.Where(b => b.FacilityId == facilityId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<BookingStatus>(status, true, out var statusEnum))
                {
                    query = query.Where(b => b.Status == statusEnum);
                }
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<bool> HasConflictAsync(string facilityId, DateTime startTime, DateTime endTime, string? excludeBookingId = null)
        {
            var query = _context.Set<Booking>()
                .Where(b => b.FacilityId == facilityId
                    && b.Status != BookingStatus.Cancelled
                    && b.Status != BookingStatus.Rejected
                    && (b.Status == BookingStatus.Approved || b.Status == BookingStatus.Pending_Approval)
                    && ((b.StartTime < endTime && b.EndTime > startTime)));

            if (!string.IsNullOrEmpty(excludeBookingId))
            {
                query = query.Where(b => b.BookingId != excludeBookingId);
            }

            return await query.AnyAsync();
        }
    }
}

