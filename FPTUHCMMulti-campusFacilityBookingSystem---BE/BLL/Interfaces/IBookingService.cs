using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<ApiResponseWithPagination<List<BookingResponseDto>>> GetAllAsync(BookingFilterDto filter);
        Task<ApiResponse<BookingResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<BookingResponseDto>> CreateAsync(CreateBookingDto dto);
        Task<ApiResponse<BookingResponseDto>> UpdateAsync(string id, UpdateBookingDto dto);
        Task<ApiResponse> CancelAsync(string bookingId, string userId, string? reason = null);
        Task<ApiResponse<BookingResponseDto>> SubmitBookingAsync(string bookingId);
        Task<ApiResponse<BookingResponseDto>> ApproveBookingAsync(string bookingId, string approverId);
        Task<ApiResponse<BookingResponseDto>> RejectBookingAsync(string bookingId, string approverId, string? reason);
        Task<ApiResponse<AvailabilityCheckResponseDto>> CheckAvailabilityAsync(CheckAvailabilityDto dto);
        Task<ApiResponse<List<AlternativeFacilityDto>>> GetAlternativeFacilitiesAsync(string facilityId, DateTime startTime, DateTime endTime, int capacity);
        Task<ApiResponse<BookingResponseDto>> CheckInAsync(string bookingId, string userId, CheckInDto? dto = null);
        Task<ApiResponse<BookingResponseDto>> CheckOutAsync(string bookingId, string userId, CheckOutDto? dto = null);
        Task ProcessLateCheckInBookingsAsync(); // hủy booking khi quá thời gian check-in
    }
}

