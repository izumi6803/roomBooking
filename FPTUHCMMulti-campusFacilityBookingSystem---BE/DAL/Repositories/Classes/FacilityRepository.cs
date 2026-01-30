using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<Facility> items, int total)> GetFilteredAsync(string? name, string? status, string? typeId, string? campusId, int page, int limit)
        {
            var query = _context.Set<Facility>()
                .Include(f => f.Campus)
                .Include(f => f.FacilityType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(f => f.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<FacilityStatus>(status, true, out var statusEnum))
                {
                    query = query.Where(f => f.Status == statusEnum);
                }
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                query = query.Where(f => f.TypeId == typeId);
            }

            if (!string.IsNullOrEmpty(campusId))
            {
                query = query.Where(f => f.CampusId == campusId);
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(f => f.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<Facility?> GetByIdWithDetailsAsync(string id)
        {
            return await _context.Set<Facility>()
                .Include(f => f.Campus)
                .Include(f => f.FacilityType)
                .Include(f => f.Bookings.Where(b => b.StartTime >= DateTime.UtcNow))
                .FirstOrDefaultAsync(f => f.FacilityId == id);
        }

        public async Task<List<Facility>> GetFacilitiesWithAvailabilityAsync(string campusId, DateTime from, DateTime to)
        {
            return await _context.Set<Facility>()
                .Include(f => f.FacilityType)
                .Include(f => f.Bookings.Where(b => 
                    b.Status != DAL.Models.Enums.BookingStatus.Cancelled &&
                    b.Status != DAL.Models.Enums.BookingStatus.Rejected &&
                    b.StartTime < to && b.EndTime > from))
                .Where(f => f.CampusId == campusId && f.Status == DAL.Models.Enums.FacilityStatus.Available)
                .OrderBy(f => f.Name)
                .ToListAsync();
        }
    }
}


