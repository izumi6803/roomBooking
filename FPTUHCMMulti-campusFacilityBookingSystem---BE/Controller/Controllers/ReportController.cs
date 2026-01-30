using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API báo cáo thống kê bookings
    /// </summary>
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "RL0003")] // Chỉ Facility_Admin
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Lấy báo cáo thống kê bookings với filter thời gian và phân tích chi tiết
        /// </summary>
        /// <param name="filter">
        /// Bộ lọc báo cáo với các tham số sau:
        /// - periodType: Loại khoảng thời gian (day/week/month/year/custom)
        /// - days: Số ngày gần nhất (1, 3, 7, 14, 30) - chỉ dùng khi periodType=day
        /// - month: Tháng (1-12) - chỉ dùng khi periodType=month
        /// - year: Năm (2024, 2025...) - dùng với periodType=month hoặc year
        /// - startDate: Ngày bắt đầu (DateTime) - chỉ dùng khi periodType=custom
        /// - endDate: Ngày kết thúc (DateTime) - chỉ dùng khi periodType=custom
        /// - campusId: Lọc theo ID campus (tùy chọn)
        /// - facilityId: Lọc theo ID facility (tùy chọn)
        /// </param>
        /// <returns>
        /// Báo cáo thống kê chi tiết bao gồm:
        /// - Period: Thông tin khoảng thời gian được phân tích
        /// - Overall: Thống kê tổng quan (tổng số booking, tỷ lệ duyệt, hủy, hoàn thành, sử dụng)
        /// - DailyStats: Thống kê chi tiết theo từng ngày
        /// - FacilityStats: Thống kê theo từng facility (kèm đánh giá trung bình)
        /// - CampusStats: Thống kê theo từng campus
        /// </returns>
        /// <response code="200">Trả về báo cáo thống kê thành công</response>
        /// <response code="401">Không có quyền truy cập (chỉ Facility_Admin)</response>
        /// <response code="500">Lỗi server khi xử lý báo cáo</response>
        /// <remarks>
        /// **Yêu cầu quyền:** Chỉ Facility_Admin (RL0003) mới có thể truy cập API này
        /// 
        /// **Mục đích:** 
        /// API này cung cấp báo cáo thống kê chi tiết về việc đặt chỗ cơ sở vật chất, 
        /// bao gồm tỷ lệ sử dụng, tỷ lệ duyệt, hủy và các chỉ số quan trọng khác.
        /// 
        /// **Các loại Period Type:**
        /// 
        /// 1. **"day"**: X ngày gần nhất
        ///    - Bắt buộc: periodType="day", days (1, 3, 7, 14, hoặc 30)
        ///    - Ví dụ: ?periodType=day&amp;days=7 (7 ngày gần nhất)
        /// 
        /// 2. **"week"**: 7 ngày gần nhất
        ///    - Bắt buộc: periodType="week"
        ///    - Ví dụ: ?periodType=week
        /// 
        /// 3. **"month"**: Theo tháng cụ thể
        ///    - Bắt buộc: periodType="month", month (1-12), year
        ///    - Ví dụ: ?periodType=month&amp;month=12&amp;year=2024 (Tháng 12/2024)
        /// 
        /// 4. **"year"**: Theo năm
        ///    - Bắt buộc: periodType="year", year
        ///    - Ví dụ: ?periodType=year&amp;year=2024 (Năm 2024)
        /// 
        /// 5. **"custom"**: Khoảng thời gian tùy chỉnh
        ///    - Bắt buộc: periodType="custom", startDate, endDate
        ///    - Ví dụ: ?periodType=custom&amp;startDate=2024-01-01&amp;endDate=2024-12-31
        /// 
        /// **Các chỉ số trong Overall Statistics:**
        /// - TotalBookings: Tổng số lượt đặt
        /// - ApprovedBookings: Số lượt đã duyệt
        /// - RejectedBookings: Số lượt bị từ chối
        /// - CancelledBookings: Số lượt đã hủy
        /// - CompletedBookings: Số lượt đã hoàn thành
        /// - PendingBookings: Số lượt đang chờ duyệt
        /// - ApprovalRate: Tỷ lệ duyệt (%)
        /// - CancellationRate: Tỷ lệ hủy (%)
        /// - CompletionRate: Tỷ lệ hoàn thành (%)
        /// - UtilizationRate: Tỷ lệ sử dụng (%)
        /// 
        /// **Ví dụ Request:**
        /// 
        /// 1. 7 ngày gần nhất:
        ///    GET /api/reports/bookings?periodType=day&amp;days=7
        /// 
        /// 2. 30 ngày gần nhất:
        ///    GET /api/reports/bookings?periodType=day&amp;days=30
        /// 
        /// 3. Tháng 12 năm 2024:
        ///    GET /api/reports/bookings?periodType=month&amp;month=12&amp;year=2024
        /// 
        /// 4. Năm 2024:
        ///    GET /api/reports/bookings?periodType=year&amp;year=2024
        /// 
        /// 5. Khoảng thời gian tùy chỉnh:
        ///    GET /api/reports/bookings?periodType=custom&amp;startDate=2024-01-01&amp;endDate=2024-12-31
        /// 
        /// 6. Lọc theo campus:
        ///    GET /api/reports/bookings?periodType=day&amp;days=7&amp;campusId=C0001
        /// 
        /// 7. Lọc theo facility:
        ///    GET /api/reports/bookings?periodType=day&amp;days=7&amp;facilityId=F00001
        /// 
        /// **Lưu ý:**
        /// - Tất cả thời gian được tính theo UTC
        /// - UtilizationRate được tính dựa trên số booking đã hoàn thành / tổng số slot có thể đặt
        /// - AverageRating trong FacilityStats được lấy từ feedback của người dùng
        /// </remarks>
        [HttpGet("bookings")]
        [ProducesResponseType(typeof(ApiResponse<BookingReportResponseDto>), 200)]
        public async Task<IActionResult> GetBookingReport([FromQuery] BookingReportFilterDto filter)
        {
            try
            {
                var result = await _reportService.GetBookingReportAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }
    }
}

