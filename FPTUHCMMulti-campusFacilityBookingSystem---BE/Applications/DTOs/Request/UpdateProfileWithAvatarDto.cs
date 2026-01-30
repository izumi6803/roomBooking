using Microsoft.AspNetCore.Http;

namespace Applications.DTOs.Request
{
    public class UpdateProfileWithAvatarDto
    {
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
