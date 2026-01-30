using BLL.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BLL.Classes
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException("Cloudinary configuration is missing. Please check appsettings.json");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string?> UploadImageAsync(IFormFile file, string folder = "bookings")
        {
            if (file == null || file.Length == 0)
                return null;

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Định dạng file không hợp lệ. Chỉ chấp nhận: {string.Join(", ", allowedExtensions)}");
            }

            // Validate file size (max 10MB)
            const long maxFileSize = 10 * 1024 * 1024; // 10MB
            if (file.Length > maxFileSize)
            {
                throw new ArgumentException("Kích thước file không được vượt quá 10MB.");
            }

            await using var stream = file.OpenReadStream();
            
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = $"facility-booking/{folder}",
                Transformation = new Transformation()
                    .Quality("auto")
                    .FetchFormat("auto")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Upload failed: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUrl?.ToString();
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folder = "bookings")
        {
            var urls = new List<string>();

            if (files == null || !files.Any())
                return urls;

            foreach (var file in files)
            {
                var url = await UploadImageAsync(file, folder);
                if (!string.IsNullOrEmpty(url))
                {
                    urls.Add(url);
                }
            }

            return urls;
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }
}
