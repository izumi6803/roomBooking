using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý xác thực và đăng nhập
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng nhập bằng Email/Username và Password
        /// </summary>
        /// <param name="dto">Thông tin đăng nhập</param>
        /// <returns>JWT token và thông tin user</returns>
        /// <response code="200">Đăng nhập thành công</response>
        /// <response code="401">Email/username hoặc mật khẩu không đúng</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(ApiResponse<AuthResponseDto>.Ok(result));
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
        /// Đăng xuất (xóa token ở client side)
        /// </summary>
        /// <returns>Kết quả đăng xuất</returns>
        /// <response code="200">Đăng xuất thành công</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// Client cần xóa JWT token khỏi storage sau khi nhận 200 OK
        /// </remarks>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        public IActionResult Logout()
        {
            return Ok(ApiResponse.Ok());
        }

        /// <summary>
        /// Đăng nhập bằng Google OAuth (chỉ @fpt.edu.vn hoặc @fe.edu.vn)
        /// </summary>
        /// <param name="dto">Google ID Token</param>
        /// <returns>JWT token và thông tin user (tự động tạo nếu lần đầu)</returns>
        /// <response code="200">Đăng nhập thành công</response>
        /// <response code="401">Email domain không hợp lệ</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Logic phân role tự động:**
        /// - @fpt.edu.vn → Student
        /// - @fe.edu.vn → Lecturer
        /// 
        /// **Lưu ý:** Nếu là lần đầu login, hệ thống tự động tạo user mới và gửi mã xác thực qua email
        /// </remarks>
        [HttpPost("login/google")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _authService.LoginWithGoogleAsync(dto);
                return Ok(ApiResponse<AuthResponseDto>.Ok(result));
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
        /// Xác thực email bằng mã 6 số
        /// </summary>
        /// <param name="email">Email cần xác thực</param>
        /// <param name="code">Mã xác thực 6 số nhận qua email</param>
        /// <returns>Kết quả xác thực</returns>
        /// <response code="200">Xác thực thành công</response>
        /// <response code="400">Mã không hợp lệ hoặc đã hết hạn</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Xác thực email sau khi đăng ký (đặc biệt với Google login)
        /// 
        /// Mã có hiệu lực 24 giờ
        /// </remarks>
        [HttpPost("verify-email")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string code)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code))
            {
                return BadRequest(ApiResponse.Fail(400, "Email và mã xác thực là bắt buộc."));
            }

            try
            {
                var result = await _authService.VerifyEmailAsync(email, code);
                if (result)
                {
                    return Ok(ApiResponse.Ok());
                }
                return BadRequest(ApiResponse.Fail(400, "Mã xác thực không hợp lệ hoặc đã hết hạn."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Gửi lại mã xác thực email (nếu mã cũ đã hết hạn)
        /// </summary>
        /// <param name="email">Email cần gửi lại mã</param>
        /// <returns>Kết quả gửi mã</returns>
        /// <response code="200">Gửi mã thành công</response>
        /// <response code="400">Email đã được xác thực</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Sử dụng khi mã xác thực cũ hết hạn (24h)
        /// </remarks>
        [HttpPost("resend-verification-email")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> ResendVerificationEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(ApiResponse.Fail(400, "Email là bắt buộc."));
            }

            try
            {
                var result = await _authService.ResendVerificationEmailAsync(email);
                if (result)
                {
                    return Ok(ApiResponse.Ok());
                }
                return BadRequest(ApiResponse.Fail(400, "Không thể gửi email xác thực."));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse.Fail(404, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Yêu cầu đặt lại mật khẩu - gửi mã 6 số qua email
        /// </summary>
        /// <param name="email">Email tài khoản cần đặt lại mật khẩu</param>
        /// <returns>Kết quả gửi mã</returns>
        /// <response code="200">Gửi mã thành công (expire sau 1 giờ)</response>
        /// <response code="400">Tài khoản dùng Google login</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Sử dụng khi user quên mật khẩu
        /// 
        /// **Lưu ý:** Không áp dụng cho tài khoản đăng nhập Google
        /// </remarks>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(ApiResponse.Fail(400, "Email là bắt buộc."));
            }

            try
            {
                var result = await _authService.ForgotPasswordAsync(email);
                if (result)
                {
                    return Ok(ApiResponse.Ok());
                }
                return BadRequest(ApiResponse.Fail(400, "Không thể gửi mã đặt lại mật khẩu."));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse.Fail(404, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse.Fail(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Đặt lại mật khẩu bằng mã 6 số
        /// </summary>
        /// <param name="email">Email tài khoản</param>
        /// <param name="code">Mã reset 6 số nhận qua email</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <returns>Kết quả đặt lại mật khẩu</returns>
        /// <response code="200">Đặt lại mật khẩu thành công</response>
        /// <response code="400">Mã không hợp lệ hoặc đã hết hạn</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Đặt lại mật khẩu sau khi nhận được mã từ forgot-password
        /// 
        /// Mã có hiệu lực 1 giờ
        /// </remarks>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string code, [FromQuery] string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(code) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest(ApiResponse.Fail(400, "Email, mã xác thực và mật khẩu mới là bắt buộc."));
            }

            try
            {
                var result = await _authService.ResetPasswordAsync(email, code, newPassword);
                if (result)
                {
                    return Ok(ApiResponse.Ok());
                }
                return BadRequest(ApiResponse.Fail(400, "Mã đặt lại mật khẩu không hợp lệ hoặc đã hết hạn."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }
    }
}
