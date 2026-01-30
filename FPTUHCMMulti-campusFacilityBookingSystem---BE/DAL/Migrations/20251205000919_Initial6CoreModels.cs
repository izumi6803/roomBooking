using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial6CoreModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "facility_type",
                columns: table => new
                {
                    type_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    DefaultAmenities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCapacity = table.Column<int>(type: "int", nullable: true),
                    TypicalDurationHours = table.Column<int>(type: "int", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility_type", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    booking_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    facility_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    purpose = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    estimated_attendees = table.Column<int>(type: "int", nullable: true),
                    special_requirements = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Draft"),
                    approved_by = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    rejection_reason = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    check_in_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    check_out_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_used = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    cancelled_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cancellation_reason = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.booking_id);
                });

            migrationBuilder.CreateTable(
                name: "campus",
                columns: table => new
                {
                    campus_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    facility_manager_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campus", x => x.campus_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    campus_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    is_verify = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Unverified"),
                    avatar_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    email_verification_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    email_verification_code_expiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    password_reset_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    password_reset_code_expiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_campus_campus_id",
                        column: x => x.campus_id,
                        principalTable: "campus",
                        principalColumn: "campus_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "facility",
                columns: table => new
                {
                    facility_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    room_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    floor_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    campus_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    type_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Available"),
                    amenities = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    facility_manager_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    max_concurrent_bookings = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility", x => x.facility_id);
                    table.ForeignKey(
                        name: "FK_facility_campus_campus_id",
                        column: x => x.campus_id,
                        principalTable: "campus",
                        principalColumn: "campus_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_facility_facility_type_type_id",
                        column: x => x.type_id,
                        principalTable: "facility_type",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_facility_user_facility_manager_id",
                        column: x => x.facility_manager_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "campus",
                columns: new[] { "campus_id", "address", "created_at", "email", "facility_manager_id", "name", "phone_number", "updated_at" },
                values: new object[,]
                {
                    { "C0001", "Lô E2a-7, Đường D1, Khu Công nghệ cao, P.Long Thạnh Mỹ, Tp. Thủ Đức, TP.HCM", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8827), "daihocfpt@fpt.edu.vn", null, "FPTU HCM Campus", "028 7300 5588", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8828) },
                    { "C0002", "Khu đô thị Khoa học và Giáo dục Quy Hòa, Phường Ghềnh Ráng, TP. Quy Nhơn, Bình Định", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8833), "daihocfpt.qn@fpt.edu.vn", null, "FPTU Quy Nhơn Campus", "0256 7300 999", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8834) }
                });

            migrationBuilder.InsertData(
                table: "facility_type",
                columns: new[] { "type_id", "created_at", "DefaultAmenities", "DefaultCapacity", "description", "IconUrl", "name", "TypicalDurationHours", "updated_at" },
                values: new object[,]
                {
                    { "FT0001", new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9499), null, null, "Phòng học lý thuyết", null, "Classroom", null, new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9500) },
                    { "FT0002", new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9503), null, null, "Phòng họp", null, "Meeting Room", null, new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9504) },
                    { "FT0003", new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9507), null, null, "Phòng máy tính", null, "Computer Lab", null, new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9507) },
                    { "FT0004", new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9510), null, null, "Sân thể thao", null, "Sports Court", null, new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9510) },
                    { "FT0005", new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9513), null, null, "Hội trường", null, "Auditorium", null, new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9513) }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "role_id", "created_at", "role_name", "updated_at" },
                values: new object[,]
                {
                    { "RL0001", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8829), "Student", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8830) },
                    { "RL0002", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8834), "Lecturer", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8835) },
                    { "RL0003", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8837), "Facility_Admin", new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8838) }
                });

            migrationBuilder.InsertData(
                table: "facility",
                columns: new[] { "facility_id", "amenities", "campus_id", "capacity", "created_at", "description", "facility_manager_id", "floor_number", "max_concurrent_bookings", "name", "room_number", "type_id", "updated_at" },
                values: new object[,]
                {
                    { "F00001", null, "C0001", 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1945), "Phòng học lý thuyết", null, "1", 1, "Phòng A101", "A101", "FT0001", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1946) },
                    { "F00002", null, "C0001", 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1952), "Phòng học lý thuyết", null, "1", 1, "Phòng A102", "A102", "FT0001", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1952) },
                    { "F00003", null, "C0001", 15, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1956), "Phòng họp nhỏ", null, "2", 1, "Phòng họp B201", "B201", "FT0002", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1957) },
                    { "F00004", null, "C0001", 25, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1961), "Phòng họp vừa", null, "2", 1, "Phòng họp B202", "B202", "FT0002", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1961) },
                    { "F00005", null, "C0001", 50, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1965), "Phòng máy 50 máy", null, "3", 1, "Lab máy tính C301", "C301", "FT0003", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1971) },
                    { "F00006", null, "C0001", 50, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1975), "Phòng máy 50 máy", null, "3", 1, "Lab máy tính C302", "C302", "FT0003", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1975) },
                    { "F00007", null, "C0001", 100, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1979), "Sân bóng rổ ngoài trời", null, "0", 2, "Sân bóng rổ", "Court1", "FT0004", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1980) },
                    { "F00008", null, "C0001", 80, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1984), "4 sân cầu lông", null, "0", 4, "Sân cầu lông", "Court2", "FT0004", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1984) },
                    { "F00009", null, "C0001", 500, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2028), "Hội trường lớn", null, "1", 1, "Hội trường A", "HallA", "FT0005", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2029) },
                    { "F00010", null, "C0001", 200, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2033), "Hội trường nhỏ", null, "1", 1, "Hội trường B", "HallB", "FT0005", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2033) },
                    { "F00011", null, "C0002", 35, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2115), "Phòng học lý thuyết", null, "1", 1, "Phòng QN101", "QN101", "FT0001", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2116) },
                    { "F00012", null, "C0002", 35, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2119), "Phòng học lý thuyết", null, "1", 1, "Phòng QN102", "QN102", "FT0001", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2120) },
                    { "F00013", null, "C0002", 12, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2124), "Phòng họp nhỏ", null, "2", 1, "Phòng họp QN201", "QN201", "FT0002", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2125) },
                    { "F00014", null, "C0002", 20, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2128), "Phòng họp vừa", null, "2", 1, "Phòng họp QN202", "QN202", "FT0002", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2129) },
                    { "F00015", null, "C0002", 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2133), "Phòng máy 40 máy", null, "3", 1, "Lab máy tính QN301", "QN301", "FT0003", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2133) },
                    { "F00016", null, "C0002", 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2137), "Phòng máy 40 máy", null, "3", 1, "Lab máy tính QN302", "QN302", "FT0003", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2137) },
                    { "F00017", null, "C0002", 50, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2141), "Sân bóng đá 5 người", null, "0", 2, "Sân bóng đá", "Court1", "FT0004", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2141) },
                    { "F00018", null, "C0002", 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2145), "2 sân tennis", null, "0", 2, "Sân tennis", "Court2", "FT0004", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2146) },
                    { "F00019", null, "C0002", 400, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2149), "Hội trường chính", null, "1", 1, "Hội trường QN", "HallQN", "FT0005", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2150) },
                    { "F00020", null, "C0002", 100, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2153), "Phòng hội thảo", null, "2", 1, "Phòng đa năng QN", "MultiQN", "FT0005", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2154) }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "user_id", "avatar_url", "campus_id", "created_at", "email", "email_verification_code", "email_verification_code_expiry", "full_name", "is_verify", "last_login", "password", "password_reset_code", "password_reset_code_expiry", "phone_number", "role_id", "updated_at", "user_name" },
                values: new object[,]
                {
                    { "U00001", null, "C0001", new DateTime(2025, 12, 5, 0, 9, 17, 907, DateTimeKind.Utc).AddTicks(6718), "student@fpt.edu.vn", null, null, "Nguyễn Văn A", "Unverified", null, "$2a$11$ZZJd00NzYPM9jhH5PpKtCOJ4d4aR64wuuKjWiQKMJxxHFoQZYbkWq", null, null, null, "RL0001", new DateTime(2025, 12, 5, 0, 9, 17, 907, DateTimeKind.Utc).AddTicks(6723), "studentA" },
                    { "U00002", null, "C0001", new DateTime(2025, 12, 5, 0, 9, 18, 112, DateTimeKind.Utc).AddTicks(4329), "lecturer@fe.edu.vn", null, null, "Trần Thị B", "Unverified", null, "$2a$11$9uDyDC0uIXxTIyHfyS0Pbu.DkQm.EKWMriuZZgZkK6/.2qyYOuY.a", null, null, null, "RL0002", new DateTime(2025, 12, 5, 0, 9, 18, 112, DateTimeKind.Utc).AddTicks(4336), "lecturerB" },
                    { "U00003", null, "C0001", new DateTime(2025, 12, 5, 0, 9, 18, 321, DateTimeKind.Utc).AddTicks(7761), "admin@fpt.edu.vn", null, null, "Admin System", "Unverified", null, "$2a$11$3O.yiPJ7fOCo9wlrUFfZ8./r2AnqYqF67uBBed5rl1LNl1BHI/G3u", null, null, null, "RL0003", new DateTime(2025, 12, 5, 0, 9, 18, 321, DateTimeKind.Utc).AddTicks(7765), "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_approved_by",
                table: "booking",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "IX_booking_facility_id",
                table: "booking",
                column: "facility_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_user_id",
                table: "booking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_campus_facility_manager_id",
                table: "campus",
                column: "facility_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_campus_name",
                table: "campus",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_facility_campus_id",
                table: "facility",
                column: "campus_id");

            migrationBuilder.CreateIndex(
                name: "IX_facility_facility_manager_id",
                table: "facility",
                column: "facility_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_facility_type_id",
                table: "facility",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_campus_id",
                table: "user",
                column: "campus_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_name",
                table: "user",
                column: "user_name",
                unique: true,
                filter: "[user_name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_facility_facility_id",
                table: "booking",
                column: "facility_id",
                principalTable: "facility",
                principalColumn: "facility_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_user_approved_by",
                table: "booking",
                column: "approved_by",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_user_user_id",
                table: "booking",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_campus_user_facility_manager_id",
                table: "campus",
                column: "facility_manager_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_campus_user_facility_manager_id",
                table: "campus");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "facility");

            migrationBuilder.DropTable(
                name: "facility_type");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "campus");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
