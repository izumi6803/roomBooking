using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý thông tin user
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICloudinaryService _cloudinaryService;

        public UserController(IUserService userService, ICloudinaryService cloudinaryService)
        {
            _userService = userService;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Lấy thông tin profile của user hiện tại
        /// </summary>
        /// <returns>Thông tin user</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="401">Chưa đăng nhập hoặc token không hợp lệ</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Xem thông tin cá nhân (profile page)
        /// 
        /// User ID lấy từ JWT token
        /// </remarks>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                         User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
            }

            var result = await _userService.GetProfileAsync(userId);

            if (result == null)
            {
                return NotFound(ApiResponse.Fail(404, "Không tìm thấy user."));
            }

            return Ok(ApiResponse<UserResponseDto>.Ok(result));
        }

        /// <summary>
        /// Cập nhật thông tin profile
        /// </summary>
        /// <param name="dto">Thông tin cần cập nhật</param>
        /// <returns>Thông tin user sau khi cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Cập nhật thông tin cá nhân (PhoneNumber, AvatarUrl, StudentId)
        /// 
        /// **Lưu ý về StudentId:**
        /// - Chỉ sinh viên (role = Student) mới cần cập nhật MSSV
        /// - MSSV phải đúng format: SE/SS/IB/MC + 6 số (2 số đầu >= 14)
        /// - Ví dụ: SE173162, SS180123, IB150001
        /// 
        /// User chỉ có thể cập nhật profile của chính mình
        /// </remarks>
        [HttpPut("profile")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse.Fail(400, string.Join("; ", errors)));
            }

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                         User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
            }

            var result = await _userService.UpdateProfileAsync(userId, dto);

            if (result == null)
            {
                return NotFound(ApiResponse.Fail(404, "Không tìm thấy user."));
            }

            return Ok(ApiResponse<UserResponseDto>.Ok(result));
        }

        /// <summary>
        /// Cập nhật profile với upload avatar trực tiếp (multipart/form-data)
        /// </summary>
        /// <param name="request">Form data chứa phoneNumber, studentId, và avatar file</param>
        /// <returns>Thông tin user sau khi cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Cập nhật profile với upload avatar trực tiếp lên Cloudinary
        /// 
        /// **Form Data:**
        /// - PhoneNumber: Số điện thoại (10 số, bắt đầu bằng 0)
        /// - StudentId: MSSV (SE/SS/IB/MC + 6 số, chỉ cho sinh viên)
        /// - Avatar: File ảnh (jpg, png, gif, webp - max 10MB)
        /// </remarks>
        [HttpPut("profile/upload")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfileWithAvatar([FromForm] UpdateProfileWithAvatarDto request)
        {
            try
            {
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                             User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
                }

                // Upload avatar lên Cloudinary nếu có
                string? avatarUrl = null;
                if (request.Avatar != null && request.Avatar.Length > 0)
                {
                    avatarUrl = await _cloudinaryService.UploadImageAsync(request.Avatar, "avatars");
                }

                // Tạo DTO với dữ liệu từ form
                var dto = new UpdateUserProfileDto
                {
                    PhoneNumber = request.PhoneNumber,
                    StudentId = request.StudentId,
                    AvatarUrl = avatarUrl
                };

                // Validate DTO manually
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
                var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(dto, validationContext, validationResults, true))
                {
                    var errors = validationResults.Select(r => r.ErrorMessage).ToList();
                    return BadRequest(ApiResponse.Fail(400, string.Join("; ", errors)));
                }

                var result = await _userService.UpdateProfileAsync(userId, dto);

                if (result == null)
                {
                    return NotFound(ApiResponse.Fail(404, "Không tìm thấy user."));
                }

                return Ok(ApiResponse<UserResponseDto>.Ok(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, $"Lỗi upload: {ex.Message}"));
            }
        }

        /// <summary>
        /// Đổi mật khẩu cho tài khoản hiện tại
        /// </summary>
        /// <param name="dto">
        /// Thông tin đổi mật khẩu bao gồm:
        /// - OldPassword: Mật khẩu hiện tại (bắt buộc)
        /// - NewPassword: Mật khẩu mới (bắt buộc, phải đáp ứng các yêu cầu bên dưới)
        /// - ConfirmPassword: Xác nhận mật khẩu mới (bắt buộc, phải khớp với NewPassword)
        /// </param>
        /// <returns>Kết quả đổi mật khẩu</returns>
        /// <response code="200">Đổi mật khẩu thành công</response>
        /// <response code="400">Mật khẩu cũ không đúng, mật khẩu mới không đáp ứng yêu cầu, hoặc dữ liệu không hợp lệ</response>
        /// <response code="401">Chưa đăng nhập hoặc mật khẩu hiện tại không đúng</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Đổi mật khẩu cho tài khoản (yêu cầu nhập mật khẩu cũ để xác thực)
        /// 
        /// **Yêu Cầu Mật Khẩu Mới:**
        /// 
        /// Mật khẩu mới phải đáp ứng **TẤT CẢ** các điều kiện sau:
        /// 
        /// 1. **Độ dài tối thiểu:** Ít nhất 8 ký tự
        /// 2. **Chữ cái viết hoa:** Phải có ít nhất 1 chữ cái viết hoa (A-Z)
        /// 3. **Chữ cái viết thường:** Phải có ít nhất 1 chữ cái viết thường (a-z)
        /// 4. **Chữ số:** Phải có ít nhất 1 chữ số (0-9)
        /// 5. **Ký tự đặc biệt:** Phải có ít nhất 1 ký tự đặc biệt trong danh sách: `!@#$%^&amp;*()_+-=[]{}|;:,.<>?`
        /// 6. **Khác mật khẩu cũ:** Mật khẩu mới phải khác mật khẩu hiện tại
        /// 7. **Xác nhận khớp:** ConfirmPassword phải khớp với NewPassword
        /// 
        /// **Ví dụ mật khẩu hợp lệ:**
        /// - `Password123!`
        /// - `MyP@ssw0rd`
        /// - `Secure#Pass2024`
        /// 
        /// **Ví dụ mật khẩu không hợp lệ:**
        /// - `password` (thiếu chữ hoa, số, ký tự đặc biệt)
        /// - `PASSWORD123!` (thiếu chữ thường)
        /// - `Password!` (thiếu chữ số)
        /// - `Password123` (thiếu ký tự đặc biệt)
        /// - `Pass1!` (chỉ có 6 ký tự, cần ít nhất 8)
        /// 
        /// **Lưu Ý Quan Trọng:**
        /// 
        /// - ⚠️ **Chỉ áp dụng cho tài khoản đăng nhập bằng email/password**
        /// - ❌ **KHÔNG áp dụng cho tài khoản đăng nhập bằng Google** (sẽ trả về lỗi 400)
        /// - 🔒 Mật khẩu cũ phải chính xác để xác thực
        /// - ✅ Sau khi đổi mật khẩu thành công, user cần đăng nhập lại với mật khẩu mới
        /// 
        /// **Các Lỗi Có Thể Xảy Ra:**
        /// 
        /// - `400`: Mật khẩu hiện tại không đúng
        /// - `400`: Mật khẩu mới phải khác mật khẩu hiện tại
        /// - `400`: Mật khẩu không đáp ứng yêu cầu (sẽ có thông báo cụ thể)
        /// - `400`: Tài khoản này sử dụng đăng nhập Google (không thể đổi mật khẩu)
        /// - `401`: Chưa đăng nhập hoặc token không hợp lệ
        /// 
        /// **Request Body Example:**
        /// ```json
        /// {
        ///   "oldPassword": "OldPass123!",
        ///   "newPassword": "NewSecurePass2024!",
        ///   "confirmPassword": "NewSecurePass2024!"
        /// }
        /// ```
        /// </remarks>
        [HttpPut("change-password")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ"));
            }

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                         User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
            }

            try
            {
                var result = await _userService.ChangePasswordAsync(userId, dto);
                if (result)
                {
                    return Ok(ApiResponse.Ok());
                }
                return BadRequest(ApiResponse.Fail(400, "Đổi mật khẩu thất bại."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse.Fail(401, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả users với filtering (Admin only)
        /// </summary>
        /// <param name="filter">Bộ lọc và pagination</param>
        /// <returns>Danh sách users</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <response code="403">Không có quyền</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Quản lý danh sách users trong hệ thống
        /// 
        /// **Filters:**
        /// - `name`: Tìm theo tên (contains)
        /// - `email`: Tìm theo email (contains)
        /// - `roleId`: Lọc theo role (RL0001, RL0002, RL0003)
        /// - `campusId`: Lọc theo campus
        /// - `status`: Active | Inactive
        /// - `page`: Trang (default: 1)
        /// - `limit`: Số items/trang (default: 10)
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<UserResponseDto>>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAll([FromQuery] UserFilterDto filter)
        {
            try
            {
                var result = await _userService.GetAllAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy chi tiết user (Admin only)
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Thông tin user</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="403">Không có quyền</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Xem chi tiết thông tin user khác
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(Roles = "RL0003")] 
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _userService.GetByIdAsync(id);
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
        /// Xóa user (soft delete - set status = Inactive)
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Kết quả xóa</returns>
        /// <response code="200">Xóa thành công</response>
        /// <response code="403">Không có quyền</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Vô hiệu hóa user account (soft delete)
        /// 
        /// **Lưu ý:** 
        /// - Không xóa hẳn khỏi database
        /// - Chỉ set status = Inactive
        /// - User không thể đăng nhập sau khi bị xóa
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);
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
