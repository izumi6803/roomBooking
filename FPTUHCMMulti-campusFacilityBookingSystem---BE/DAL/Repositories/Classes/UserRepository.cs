using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdWithRoleAsync(string id)
        {
            return await _context.Set<User>()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => 
                (u.Email != null && u.Email == emailOrUsername) || 
                (u.UserName != null && u.UserName == emailOrUsername));
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Set<User>().AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Set<User>().AnyAsync(u => u.UserName == username);
        }

        public async Task<int> AddAsync(User entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<(List<User> items, int total)> GetFilteredAsync(string? name, string? email, string? roleId, string? status, int page, int limit)
        {
            var query = _context.Set<User>()
                .Include(u => u.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(u => u.FullName.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email != null && u.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                query = query.Where(u => u.RoleId == roleId);
            }


            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<UserStatus>(status, true, out var statusEnum))
                {
                    query = query.Where(u => u.Status == statusEnum);
                }
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
    }
}

