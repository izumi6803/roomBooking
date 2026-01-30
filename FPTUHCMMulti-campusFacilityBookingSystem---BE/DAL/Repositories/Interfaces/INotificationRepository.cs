using DAL.Models;
using DAL.Models.Enums;

namespace DAL.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification?> GetByIdAsync(string id);
        Task<List<Notification>> GetAllAsync();
        Task<(List<Notification> items, int total)> GetFilteredAsync(
            string? userId,
            NotificationType? type,
            NotificationStatus? status,
            int page,
            int limit);
        Task<List<Notification>> GetUnreadByUserIdAsync(string userId);
        Task<int> GetUnreadCountByUserIdAsync(string userId);
        Task<int> CreateAsync(Notification notification);
        Task AddRangeAsync(List<Notification> notifications);
        Task MarkAsReadAsync(string notificationId);
        Task MarkAllAsReadByUserIdAsync(string userId);
        Task<bool> RemoveAsync(Notification notification);
    }
}

