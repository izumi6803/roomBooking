using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class SystemSettingsRepository : GenericRepository<SystemSettings>, ISystemSettingsRepository
    {
        public SystemSettingsRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<SystemSettings?> GetByKeyAsync(string key)
        {
            return await _context.Set<SystemSettings>()
                .FirstOrDefaultAsync(s => s.SettingKey == key);
        }

        public async Task<int> CreateOrUpdateAsync(string key, string value, string? description = null)
        {
            var existing = await GetByKeyAsync(key);
            if (existing != null)
            {
                existing.SettingValue = value;
                if (description != null)
                    existing.Description = description;
                existing.UpdatedAt = DateTime.UtcNow;
                _context.Set<SystemSettings>().Update(existing);
            }
            else
            {
                var newSetting = new SystemSettings
                {
                    SettingKey = key,
                    SettingValue = value,
                    Description = description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _context.Set<SystemSettings>().AddAsync(newSetting);
            }
            return await _context.SaveChangesAsync();
        }
    }
}

