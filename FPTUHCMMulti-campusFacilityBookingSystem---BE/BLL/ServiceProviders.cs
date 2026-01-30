using BLL.Classes;
using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceProviders
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Auth & User Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            // Core Business Services
            services.AddScoped<ICampusService, CampusService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFacilityTypeService, FacilityTypeService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingFeedbackService, BookingFeedbackService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ISystemSettingsService, SystemSettingsService>();

            // Cloud Services
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            // Background Services
            services.AddHostedService<NotificationBackgroundService>();
        }
    }
}
