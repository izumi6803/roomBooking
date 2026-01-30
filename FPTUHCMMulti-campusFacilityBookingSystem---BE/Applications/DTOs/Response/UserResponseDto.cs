namespace Applications.DTOs.Response
{
    public class UserResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; }
        public string? UserName { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string IsVerify { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
