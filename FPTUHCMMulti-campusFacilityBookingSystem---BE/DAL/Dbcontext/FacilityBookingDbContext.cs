using DAL.Models;
using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Dbcontext
{
    public class FacilityBookingDbContext : DbContext
    {
        public FacilityBookingDbContext(DbContextOptions<FacilityBookingDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingFeedback> BookingFeedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SystemSettings> SystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================
            // ROLE CONFIGURATION
            // ====================
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Seed data: 3 roles cố định
                entity.HasData(
                    new Role { RoleId = "RL0001", RoleName = "Sinh viên", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Role { RoleId = "RL0002", RoleName = "Giảng viên", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Role { RoleId = "RL0003", RoleName = "Quản trị viên cơ sở vật chất", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );
            });

            // ====================
            // CAMPUS CONFIGURATION
            // ====================
            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("campus");
                entity.HasKey(e => e.CampusId);
                entity.Property(e => e.CampusId)
                    .HasColumnName("campus_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20);
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(CampusStatus.Active);
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Indexes
                entity.HasIndex(e => e.Name).IsUnique();


                // Seed data: 2 campuses
                entity.HasData(
                    new Campus
                    {
                        CampusId = "C0001",
                        Name = "FPTU HCM Campus",
                        Address = "Lô E2a-7, Đường D1, Khu Công nghệ cao, P.Long Thạnh Mỹ, Tp. Thủ Đức, TP.HCM",
                        PhoneNumber = "028 7300 5588",
                        Email = "daihocfpt@fpt.edu.vn",
                        Status = CampusStatus.Active,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Campus
                    {
                        CampusId = "C0002",
                        Name = "Nhà Văn Hóa Sinh Viên",
                        Address = "Số 1 Lưu Hữu Phước, Đông Hoà, Dĩ An, TP.HCM",
                        PhoneNumber = "028 7300 5589",
                        Email = "nvhsv@fpt.edu.vn",
                        Status = CampusStatus.Active,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });

            // ====================
            // FACILITY TYPE CONFIGURATION
            // ====================
            modelBuilder.Entity<FacilityType>(entity =>
            {
                entity.ToTable("facility_type");
                entity.HasKey(e => e.TypeId);
                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasDefaultValue(DAL.Models.Enums.FacilityTypeStatus.Active);

                // Seed data: 5 loại cơ sở
                entity.HasData(

                    new FacilityType { TypeId = "FT0001", Name = "Phòng học", Description = "Phòng học lý thuyết", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new FacilityType { TypeId = "FT0002", Name = "Phòng họp", Description = "Phòng họp", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new FacilityType { TypeId = "FT0003", Name = "Phòng máy tính", Description = "Phòng máy tính", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new FacilityType { TypeId = "FT0004", Name = "Sân thể thao", Description = "Sân thể thao", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }

                );
            });

            // ====================
            // USER CONFIGURATION
            // ====================
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);
                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20);
                entity.Property(e => e.StudentId)
                    .HasColumnName("student_id")
                    .HasMaxLength(8);
                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(100);
                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255);
                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(UserStatus.Active);
                entity.Property(e => e.IsVerify)
                    .HasColumnName("is_verify")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(VerificationStatus.Unverified);
                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login");
                entity.Property(e => e.EmailVerificationCode)
                    .HasColumnName("email_verification_code")
                    .HasMaxLength(10);
                entity.Property(e => e.EmailVerificationCodeExpiry)
                    .HasColumnName("email_verification_code_expiry");
                entity.Property(e => e.PasswordResetCode)
                    .HasColumnName("password_reset_code")
                    .HasMaxLength(10);
                entity.Property(e => e.PasswordResetCodeExpiry)
                    .HasColumnName("password_reset_code_expiry");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Indexes
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();

                // Foreign Keys
                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Seed data: 3 users mẫu
                entity.HasData(
                    new User
                    {
                        UserId = "U00001",
                        Email = "student@fpt.edu.vn",
                        FullName = "Nguyễn Văn A",
                        UserName = "studentA",
                        Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                        RoleId = "RL0001",
                        Status = UserStatus.Active,
                        IsVerify = VerificationStatus.Unverified,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        UserId = "U00002",
                        Email = "lecturer@fe.edu.vn",
                        FullName = "Trần Thị B",
                        UserName = "lecturerB",
                        Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                        RoleId = "RL0002",
                        Status = UserStatus.Active,
                        IsVerify = VerificationStatus.Unverified,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        UserId = "U00003",
                        Email = "admin@fpt.edu.vn",
                        FullName = "Quản trị viên hệ thống",
                        UserName = "admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        RoleId = "RL0003",
                        Status = UserStatus.Active,
                        IsVerify = VerificationStatus.Unverified,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            });

            // ====================
            // FACILITY CONFIGURATION
            // ====================
            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facility");
                entity.HasKey(e => e.FacilityId);
                entity.Property(e => e.FacilityId)
                    .HasColumnName("facility_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.Capacity)
                    .HasColumnName("capacity")
                    .IsRequired();
                entity.Property(e => e.RoomNumber)
                    .HasColumnName("room_number")
                    .HasMaxLength(50);
                entity.Property(e => e.FloorNumber)
                    .HasColumnName("floor_number")
                    .HasMaxLength(10);
                entity.Property(e => e.CampusId)
                    .HasColumnName("campus_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(FacilityStatus.Available);
                entity.Property(e => e.Amenities)
                    .HasColumnName("amenities")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.FacilityManagerId)
                    .HasColumnName("facility_manager_id")
                    .HasMaxLength(6);
                entity.Property(e => e.MaxConcurrentBookings)
                    .HasColumnName("max_concurrent_bookings")
                    .HasDefaultValue(1);
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Foreign Keys
                entity.HasOne(f => f.Campus)
                    .WithMany(c => c.Facilities)
                    .HasForeignKey(f => f.CampusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.FacilityType)
                    .WithMany(ft => ft.Facilities)
                    .HasForeignKey(f => f.TypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.FacilityManager)
                    .WithMany(u => u.ManagedFacilities)
                    .HasForeignKey(f => f.FacilityManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Seed data: 20 facilities (10 cho HCM Campus, 10 cho NVHSV)
                var facilities = new List<Facility>();
                
                // HCM Campus - 10 facilities (phòng học, lab, sân thể thao)
                facilities.AddRange(new[]
                {
                    new Facility { FacilityId = "F00001", Name = "Phòng A101", Description = "Phòng học lý thuyết", Capacity = 40, RoomNumber = "A101", FloorNumber = "1", CampusId = "C0001", TypeId = "FT0001", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00002", Name = "Phòng A102", Description = "Phòng học lý thuyết", Capacity = 40, RoomNumber = "A102", FloorNumber = "1", CampusId = "C0001", TypeId = "FT0001", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00003", Name = "Phòng họp B201", Description = "Phòng họp nhỏ", Capacity = 15, RoomNumber = "B201", FloorNumber = "2", CampusId = "C0001", TypeId = "FT0002", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00004", Name = "Phòng họp B202", Description = "Phòng họp vừa", Capacity = 25, RoomNumber = "B202", FloorNumber = "2", CampusId = "C0001", TypeId = "FT0002", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00005", Name = "Lab máy tính C301", Description = "Phòng máy 50 máy", Capacity = 50, RoomNumber = "C301", FloorNumber = "3", CampusId = "C0001", TypeId = "FT0003", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00006", Name = "Lab máy tính C302", Description = "Phòng máy 50 máy", Capacity = 50, RoomNumber = "C302", FloorNumber = "3", CampusId = "C0001", TypeId = "FT0003", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00007", Name = "Sân bóng rổ", Description = "Sân bóng rổ ngoài trời", Capacity = 100, RoomNumber = "Court1", FloorNumber = "0", CampusId = "C0001", TypeId = "FT0004", Status = FacilityStatus.Available, MaxConcurrentBookings = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00008", Name = "Sân cầu lông", Description = "4 sân cầu lông", Capacity = 80, RoomNumber = "Court2", FloorNumber = "0", CampusId = "C0001", TypeId = "FT0004", Status = FacilityStatus.Available, MaxConcurrentBookings = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                });

                // NVHSV (Nhà Văn Hóa Sinh Viên) - 10 facilities
                facilities.AddRange(new[]
                {
                    new Facility { FacilityId = "F00011", Name = "Phòng N101", Description = "Phòng sinh hoạt câu lạc bộ", Capacity = 30, RoomNumber = "N101", FloorNumber = "1", CampusId = "C0002", TypeId = "FT0001", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00012", Name = "Phòng N102", Description = "Phòng sinh hoạt câu lạc bộ", Capacity = 30, RoomNumber = "N102", FloorNumber = "1", CampusId = "C0002", TypeId = "FT0001", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00013", Name = "Phòng họp NVHSV 201", Description = "Phòng họp Ban chủ nhiệm", Capacity = 15, RoomNumber = "N201", FloorNumber = "2", CampusId = "C0002", TypeId = "FT0002", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00014", Name = "Phòng họp NVHSV 202", Description = "Phòng họp CLB", Capacity = 25, RoomNumber = "N202", FloorNumber = "2", CampusId = "C0002", TypeId = "FT0002", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00015", Name = "Phòng Media NVHSV", Description = "Phòng sản xuất nội dung", Capacity = 20, RoomNumber = "N301", FloorNumber = "3", CampusId = "C0002", TypeId = "FT0003", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00016", Name = "Phòng Âm nhạc", Description = "Phòng tập nhạc, ca hát", Capacity = 25, RoomNumber = "N302", FloorNumber = "3", CampusId = "C0002", TypeId = "FT0003", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00017", Name = "Sân khấu ngoài trời", Description = "Sân khấu tổ chức sự kiện", Capacity = 200, RoomNumber = "Stage1", FloorNumber = "0", CampusId = "C0002", TypeId = "FT0004", Status = FacilityStatus.Available, MaxConcurrentBookings = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Facility { FacilityId = "F00018", Name = "Khu vực BBQ", Description = "Khu vực nướng ngoài trời", Capacity = 50, RoomNumber = "BBQ1", FloorNumber = "0", CampusId = "C0002", TypeId = "FT0004", Status = FacilityStatus.Available, MaxConcurrentBookings = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                });

                entity.HasData(facilities);
            });

            // ====================
            // BOOKING CONFIGURATION
            // ====================
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.FacilityId)
                    .HasColumnName("facility_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .IsRequired();
                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .IsRequired();
                entity.Property(e => e.Purpose)
                    .HasColumnName("purpose")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100);
                entity.Property(e => e.EstimatedAttendees)
                    .HasColumnName("estimated_attendees");
                entity.Property(e => e.SpecialRequirements)
                    .HasColumnName("special_requirements")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(BookingStatus.Pending_Approval);
                entity.Property(e => e.ApprovedBy)
                    .HasColumnName("approved_by")
                    .HasMaxLength(6);
                entity.Property(e => e.ApprovedAt)
                    .HasColumnName("approved_at");
                entity.Property(e => e.RejectionReason)
                    .HasColumnName("rejection_reason")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CheckInTime)
                    .HasColumnName("check_in_time");
                entity.Property(e => e.CheckInNote)
                    .HasColumnName("check_in_note")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CheckInImages)
                    .HasColumnName("check_in_images")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CheckOutTime)
                    .HasColumnName("check_out_time");
                entity.Property(e => e.CheckOutNote)
                    .HasColumnName("check_out_note")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CheckOutImages)
                    .HasColumnName("check_out_images")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.IsUsed)
                    .HasColumnName("is_used")
                    .HasDefaultValue(false);
                entity.Property(e => e.CancelledAt)
                    .HasColumnName("cancelled_at");
                entity.Property(e => e.CancellationReason)
                    .HasColumnName("cancellation_reason")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Foreign Keys
                entity.HasOne(b => b.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Facility)
                    .WithMany(f => f.Bookings)
                    .HasForeignKey(b => b.FacilityId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Approver)
                    .WithMany(u => u.ApprovedBookings)
                    .HasForeignKey(b => b.ApprovedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ====================
            // BOOKING FEEDBACK CONFIGURATION
            // ====================
            modelBuilder.Entity<BookingFeedback>(entity =>
            {
                entity.ToTable("booking_feedback");
                entity.HasKey(e => e.FeedbackId);
                entity.Property(e => e.FeedbackId)
                    .HasColumnName("feedback_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .IsRequired();
                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.ReportIssue)
                    .HasColumnName("report_issue")
                    .HasDefaultValue(false);
                entity.Property(e => e.IssueDescription)
                    .HasColumnName("issue_description")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ResolvedAt)
                    .HasColumnName("resolved_at");

                // Foreign Keys
                entity.HasOne(f => f.Booking)
                    .WithOne(b => b.Feedback) // 1-to-1 relationship
                    .HasForeignKey<BookingFeedback>(f => f.BookingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.User)
                    .WithMany()
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Unique constraint: One feedback per booking (bookingId is unique)
                entity.HasIndex(e => e.BookingId).IsUnique();
            });

            // ====================
            // NOTIFICATION CONFIGURATION
            // ====================
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");
                entity.HasKey(e => e.NotificationId);
                entity.Property(e => e.NotificationId)
                    .HasColumnName("notification_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(6)
                    .IsRequired();
                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .IsRequired();
                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasConversion<string>()
                    .HasDefaultValue(NotificationStatus.Unread);
                entity.Property(e => e.BookingId)
                    .HasColumnName("booking_id")
                    .HasMaxLength(6);
                entity.Property(e => e.FeedbackId)
                    .HasColumnName("feedback_id")
                    .HasMaxLength(6);
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ReadAt)
                    .HasColumnName("read_at");

                // Foreign Keys
                entity.HasOne(n => n.User)
                    .WithMany()
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(n => n.Booking)
                    .WithMany()
                    .HasForeignKey(n => n.BookingId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(n => n.Feedback)
                    .WithMany()
                    .HasForeignKey(n => n.FeedbackId)
                    .OnDelete(DeleteBehavior.NoAction);

                // Indexes
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
            });

            // ====================
            // SYSTEM SETTINGS CONFIGURATION
            // ====================
            modelBuilder.Entity<SystemSettings>(entity =>
            {
                entity.ToTable("system_settings");
                entity.HasKey(e => e.SettingKey);
                entity.Property(e => e.SettingKey)
                    .HasColumnName("setting_key")
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.SettingValue)
                    .HasColumnName("setting_value")
                    .HasMaxLength(500)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(MAX)");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                // Indexes
                entity.HasIndex(e => e.SettingKey).IsUnique();
            });
        }
    }
}
