using DAL.Models.Enums;

namespace DAL.Models
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? StudentId { get; set; } // MSSV
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string RoleId { get; set; } = string.Empty;
        public UserStatus Status { get; set; } = UserStatus.Active;
        public VerificationStatus IsVerify { get; set; } = VerificationStatus.Unverified;
        public string? AvatarUrl { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationCodeExpiry { get; set; }
        public string? PasswordResetCode { get; set; }
        public DateTime? PasswordResetCodeExpiry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Role Role { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Booking> ApprovedBookings { get; set; } = new List<Booking>();
        public ICollection<Facility> ManagedFacilities { get; set; } = new List<Facility>(); // Facilities mà user này quản lý
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
