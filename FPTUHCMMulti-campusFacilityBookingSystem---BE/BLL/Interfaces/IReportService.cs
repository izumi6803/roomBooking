using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        Task<ApiResponse<BookingReportResponseDto>> GetBookingReportAsync(BookingReportFilterDto filter);
    }
}