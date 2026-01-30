using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface ISystemSettingsService
    {
        Task<ApiResponse<SystemSettingsResponseDto>> GetSettingsAsync();
        Task<ApiResponse<SystemSettingsResponseDto>> UpdateSettingsAsync(UpdateSystemSettingsDto dto);
        
        // Helper methods để lấy giá trị settings (dùng trong BookingService)
        Task<int> GetMinimumBookingHoursBeforeStartAsync();
        Task<int> GetCheckInMinutesBeforeStartAsync();
        Task<int> GetCheckInMinutesAfterStartAsync();

        /// <summary>
        /// lấy số phút tối thiểu sau khi check-in để được phép check-out
        /// mặc định: 0 (có thể check-out ngay sau khi check-in)
        /// </summary>
        Task<int> GetCheckoutMinMinutesAfterCheckInAsync();
    }
}

