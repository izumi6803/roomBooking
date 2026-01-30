using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý cơ sở (Campuses)
    /// </summary>
    [ApiController]
    [Route("api/campuses")]
    public class CampusController : ControllerBase
    {
        private readonly ICampusService _campusService;
        private readonly IFacilityService _facilityService;
        private readonly ICloudinaryService _cloudinaryService;

        public CampusController(ICampusService campusService, IFacilityService facilityService, ICloudinaryService cloudinaryService)
        {
            _campusService = campusService;
            _facilityService = facilityService;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Lấy danh sách tất cả campuses (dùng cho dropdown selection)
        /// </summary>
        /// <returns>Danh sách campuses</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Hiển thị dropdown chọn campus trên FacilityPage
        /// 
        /// Chỉ trả về các campus Active
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<List<CampusResponseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _campusService.GetAllCampusesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách facilities trong campus với thông tin availability
        /// </summary>
        /// <param name="campusId">Campus ID</param>
        /// <param name="from">Thời gian bắt đầu kiểm tra (optional, default: now)</param>
        /// <param name="to">Thời gian kết thúc kiểm tra (optional, default: now + 24h)</param>
        /// <returns>Danh sách facilities kèm booked slots</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Xem facilities khả dụng trong campus theo khoảng thời gian
        /// 
        /// **Thông tin trả về:**
        /// - Danh sách facilities trong campus
        /// - Trạng thái availability
        /// - Danh sách các slot đã được đặt trong khoảng thời gian
        /// 
        /// **Ví dụ:**
        /// /api/campuses/C0001/facilities?from=2024-12-10T08:00:00&amp;to=2024-12-10T18:00:00
        /// </remarks>
        [HttpGet("{campusId}/facilities")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<List<FacilityAvailabilityDto>>), 200)]
        public async Task<IActionResult> GetCampusFacilities(string campusId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            try
            {
                var fromTime = from ?? DateTime.UtcNow;
                var toTime = to ?? DateTime.UtcNow.AddHours(24);

                var result = await _facilityService.GetFacilitiesWithAvailabilityAsync(campusId, fromTime, toTime);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy chi tiết campus
        /// </summary>
        /// <param name="id">Campus ID</param>
        /// <returns>Thông tin campus</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="404">Không tìm thấy campus</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// </remarks>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<CampusResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _campusService.GetByIdAsync(id);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách campus với pagination (admin)
        /// </summary>
        /// <param name="request">Pagination parameters</param>
        /// <returns>Danh sách campus với pagination</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Quản lý danh sách campus (admin view)
        /// </remarks>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<CampusResponseDto>>), 200)]
        public async Task<IActionResult> GetAllPaged([FromQuery] PagedRequestDto request)
        {
            try
            {
                var result = await _campusService.GetAllAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Tạo campus mới
        /// </summary>
        /// <param name="dto">Thông tin campus</param>
        /// <returns>Campus đã tạo</returns>
        /// <response code="200">Tạo thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<CampusResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateCampusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _campusService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Tạo campus mới kèm upload ảnh lên Cloudinary
        /// </summary>
        /// <param name="dto">Thông tin campus (form-data)</param>
        /// <param name="image">File ảnh campus (form-data field: image)</param>
        /// <returns>Campus đã tạo</returns>
        /// <response code="200">Tạo thành công</response>
        /// <response code="400">Dữ liệu hoặc file không hợp lệ</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Content-Type:** multipart/form-data
        /// 
        /// **Form fields:**
        /// - name (required)
        /// - address, phoneNumber, email, status (optional)
        /// - image (optional)
        /// 
        /// Ảnh sẽ được upload lên Cloudinary và `ImageUrl` sẽ lưu `secure_url`.
        /// </remarks>
        [HttpPost("with-image")]
        [Authorize(Roles = "RL0003")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<CampusResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateWithImage([FromForm] CreateCampusDto dto, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                if (image != null && image.Length > 0)
                {
                    var url = await _cloudinaryService.UploadImageAsync(image, "campuses");
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        return BadRequest(ApiResponse.Fail(400, "Upload ảnh campus thất bại."));
                    }

                    dto.ImageUrl = url;
                }

                var result = await _campusService.CreateAsync(dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin campus
        /// </summary>
        /// <param name="id">Campus ID</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <param name="status">Trạng thái campus (dropdown)</param>
        /// <returns>Campus đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="404">Không tìm thấy campus</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Status Options:** Active | Inactive
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<CampusResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCampusDto dto, [FromQuery] CampusStatus? status)
        {
            try
            {
                if (status.HasValue)
                    dto.Status = status.Value;
                    
                var result = await _campusService.UpdateAsync(id, dto);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Vô hiệu hóa campus (soft delete - set status = Inactive)
        /// </summary>
        /// <param name="id">Campus ID</param>
        /// <returns>Kết quả xóa</returns>
        /// <response code="200">Vô hiệu hóa thành công</response>
        /// <response code="404">Không tìm thấy campus</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _campusService.DeleteAsync(id);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }
    }
}
