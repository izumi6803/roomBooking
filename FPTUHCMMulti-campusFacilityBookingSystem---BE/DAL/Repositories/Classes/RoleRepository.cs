using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<Role> items, int total)> GetPagedAsync(int page, int limit)
        {
            var query = _context.Set<Role>().AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
    }
}


