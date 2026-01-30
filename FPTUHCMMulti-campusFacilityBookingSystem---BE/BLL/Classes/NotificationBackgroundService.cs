using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BLL.Classes
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Check every 5 minutes

        public NotificationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<NotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NotificationBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                        var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                        // Process No_Show bookings (check every 5 minutes)
                        await notificationService.ProcessNoShowBookingsAsync();
                        _logger.LogInformation("Processed No_Show bookings.");

                        // process late check-in bookings (check every 5 minutes)
                        // hàm này tự động hủy các booking đã được duyệt nhưng không check-in đúng giờ
                        // ràng buộc: booking phải check-in trong khoảng thời gian cho phép (lấy từ system settings)
                        // nếu quá thời gian, booking sẽ bị hủy với lý do "quá thời gian check-in"
                        // user sẽ nhận notification về việc booking bị hủy
                        await bookingService.ProcessLateCheckInBookingsAsync();
                        _logger.LogInformation("Processed late check-in bookings.");

                        // Create reminder notifications (check every 5 minutes)
                        await notificationService.CreateBookingReminderNotificationsAsync();
                        _logger.LogInformation("Created booking reminder notifications.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in NotificationBackgroundService.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("NotificationBackgroundService is stopping.");
        }
    }
}

