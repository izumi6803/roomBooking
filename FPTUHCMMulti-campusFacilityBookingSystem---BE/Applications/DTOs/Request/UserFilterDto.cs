using DAL.Models.Enums;

namespace Applications.DTOs.Request
{
    public class UserFilterDto : PagedRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? RoleId { get; set; }
        public UserStatus? Status { get; set; }
    }
}
