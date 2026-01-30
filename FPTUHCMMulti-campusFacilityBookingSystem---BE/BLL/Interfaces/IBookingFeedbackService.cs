using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IBookingFeedbackService
    {
        Task<ApiResponse<BookingFeedbackResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetAllAsync();
        Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetByBookingIdAsync(string bookingId);
        Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetByUserIdAsync(string userId);
        Task<ApiResponseWithPagination<List<BookingFeedbackResponseDto>>> GetFilteredAsync(BookingFeedbackFilterDto filter);
        Task<ApiResponse<double>> GetAverageFacilityRatingAsync(string facilityId);
        Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetUnresolvedIssuesAsync();
        Task<ApiResponse<BookingFeedbackResponseDto>> CreateAsync(string userId, CreateBookingFeedbackDto dto);
        Task<ApiResponse<BookingFeedbackResponseDto>> UpdateAsync(string id, string userId, UpdateBookingFeedbackDto dto);
        Task<ApiResponse<BookingFeedbackResponseDto>> ResolveIssueAsync(string id, string adminUserId);
        Task<ApiResponse> DeleteAsync(string id, string userId);
    }
}

