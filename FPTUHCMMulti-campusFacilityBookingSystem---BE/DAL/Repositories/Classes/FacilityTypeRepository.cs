using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class FacilityTypeRepository : GenericRepository<FacilityType>, IFacilityTypeRepository
    {
        public FacilityTypeRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<FacilityType> items, int total)> GetPagedAsync(int page, int limit)
        {
            var query = _context.Set<FacilityType>()
                .Where(ft => ft.Status == DAL.Models.Enums.FacilityTypeStatus.Active)
                .AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(ft => ft.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<int> DeleteAsync(FacilityType entity)
        {
            PrepareRemove(entity);
            return await SaveAsync();
        }
    }
}


