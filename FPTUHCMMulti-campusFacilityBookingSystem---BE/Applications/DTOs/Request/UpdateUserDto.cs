namespace Applications.DTOs.Request
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleId { get; set; }
        public string? CampusId { get; set; }
        public string? Status { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

