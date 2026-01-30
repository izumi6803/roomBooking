using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICloudinaryService
    {
        /// <summary>
        /// Upload một file ảnh lên Cloudinary
        /// </summary>
        /// <param name="file">File ảnh cần upload</param>
        /// <param name="folder">Thư mục trên Cloudinary (check-in, check-out, etc.)</param>
        /// <returns>URL của ảnh đã upload</returns>
        Task<string?> UploadImageAsync(IFormFile file, string folder = "bookings");

        /// <summary>
        /// Upload nhiều file ảnh lên Cloudinary
        /// </summary>
        /// <param name="files">Danh sách file ảnh</param>
        /// <param name="folder">Thư mục trên Cloudinary</param>
        /// <returns>Danh sách URLs của các ảnh đã upload</returns>
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folder = "bookings");

        /// <summary>
        /// Xóa ảnh trên Cloudinary
        /// </summary>
        /// <param name="publicId">Public ID của ảnh</param>
        /// <returns>True nếu xóa thành công</returns>
        Task<bool> DeleteImageAsync(string publicId);
    }
}
