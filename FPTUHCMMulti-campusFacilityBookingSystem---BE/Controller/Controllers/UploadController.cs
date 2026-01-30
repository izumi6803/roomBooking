using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API upload file lên Cloudinary
    /// </summary>
    [ApiController]
    [Route("api/upload")]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Upload một ảnh lên Cloudinary
        /// </summary>
        /// <param name="file">File ảnh cần upload</param>
        /// <param name="folder">Thư mục (check-in, check-out, facilities)</param>
        /// <returns>URL của ảnh đã upload</returns>
        /// <response code="200">Upload thành công</response>
        /// <response code="400">File không hợp lệ</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Định dạng file hỗ trợ:** jpg, jpeg, png, gif, webp
        /// 
        /// **Kích thước tối đa:** 10MB
        /// 
        /// **Folders:**
        /// - check-in: Ảnh check-in booking
        /// - check-out: Ảnh check-out booking
        /// - facilities: Ảnh facilities
        /// </remarks>
        [HttpPost("image")]
        [ProducesResponseType(typeof(ApiResponse<UploadResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromQuery] string folder = "bookings")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse.Fail(400, "Vui lòng chọn file để upload."));
                }

                var url = await _cloudinaryService.UploadImageAsync(file, folder);
                
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest(ApiResponse.Fail(400, "Upload thất bại."));
                }

                var response = new UploadResponseDto
                {
                    Url = url,
                    FileName = file.FileName,
                    Size = file.Length
                };

                return Ok(ApiResponse<UploadResponseDto>.Ok(response));
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
        /// Upload nhiều ảnh lên Cloudinary
        /// </summary>
        /// <param name="files">Danh sách file ảnh</param>
        /// <param name="folder">Thư mục (check-in, check-out, facilities)</param>
        /// <returns>Danh sách URLs của các ảnh đã upload</returns>
        /// <response code="200">Upload thành công</response>
        /// <response code="400">Files không hợp lệ</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Định dạng file hỗ trợ:** jpg, jpeg, png, gif, webp
        /// 
        /// **Kích thước tối đa mỗi file:** 10MB
        /// 
        /// **Số lượng tối đa:** 5 files
        /// </remarks>
        [HttpPost("images")]
        [ProducesResponseType(typeof(ApiResponse<MultiUploadResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UploadImages(List<IFormFile> files, [FromQuery] string folder = "bookings")
        {
            try
            {
                if (files == null || !files.Any())
                {
                    return BadRequest(ApiResponse.Fail(400, "Vui lòng chọn ít nhất 1 file để upload."));
                }

                if (files.Count > 5)
                {
                    return BadRequest(ApiResponse.Fail(400, "Chỉ được upload tối đa 5 files cùng lúc."));
                }

                var urls = await _cloudinaryService.UploadImagesAsync(files, folder);

                var response = new MultiUploadResponseDto
                {
                    Urls = urls,
                    TotalFiles = files.Count,
                    SuccessCount = urls.Count,
                    FailedCount = files.Count - urls.Count
                };

                return Ok(ApiResponse<MultiUploadResponseDto>.Ok(response));
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
    }
}
