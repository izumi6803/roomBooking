using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class CampusRepository : GenericRepository<Campus>, ICampusRepository
    {
        public CampusRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<Campus> items, int total)> GetPagedAsync(int page, int limit)
        {
            var query = _context.Set<Campus>().AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<List<Campus>> GetAllCampusesAsync()
        {
            return await _context.Set<Campus>()
                .Where(c => c.Status == CampusStatus.Active)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}


