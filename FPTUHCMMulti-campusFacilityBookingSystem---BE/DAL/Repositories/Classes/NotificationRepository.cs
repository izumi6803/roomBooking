using DAL.Basic;
using DAL.Dbcontext;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(FacilityBookingDbContext context) : base(context)
        {
        }

        public async Task<(List<Notification> items, int total)> GetFilteredAsync(
            string? userId,
            NotificationType? type,
            NotificationStatus? status,
            int page,
            int limit)
        {
            var query = _context.Set<Notification>()
                .Include(n => n.User)
                .Include(n => n.Booking)
                .Include(n => n.Feedback)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(n => n.UserId == userId);
            }

            if (type.HasValue)
            {
                query = query.Where(n => n.Type == type.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(n => n.Status == status.Value);
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }

        public async Task<List<Notification>> GetUnreadByUserIdAsync(string userId)
        {
            return await _context.Set<Notification>()
                .Where(n => n.UserId == userId && n.Status == NotificationStatus.Unread)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountByUserIdAsync(string userId)
        {
            return await _context.Set<Notification>()
                .CountAsync(n => n.UserId == userId && n.Status == NotificationStatus.Unread);
        }

        public async Task MarkAsReadAsync(string notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.Status = NotificationStatus.Read;
                notification.ReadAt = DateTime.UtcNow;
                await UpdateAsync(notification);
            }
        }

        public async Task MarkAllAsReadByUserIdAsync(string userId)
        {
            var unreadNotifications = await _context.Set<Notification>()
                .Where(n => n.UserId == userId && n.Status == NotificationStatus.Unread)
                .ToListAsync();

            foreach (var notification in unreadNotifications)
            {
                notification.Status = NotificationStatus.Read;
                notification.ReadAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public new async Task<int> CreateAsync(Notification notification)
        {
            return await base.CreateAsync(notification);
        }

        public async Task AddRangeAsync(List<Notification> notifications)
        {
            await _context.Set<Notification>().AddRangeAsync(notifications);
        }

        public new async Task<bool> RemoveAsync(Notification notification)
        {
            return await base.RemoveAsync(notification);
        }
    }
}

