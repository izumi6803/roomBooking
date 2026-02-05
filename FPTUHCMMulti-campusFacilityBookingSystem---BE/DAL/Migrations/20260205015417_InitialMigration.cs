using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "campus",
                columns: table => new
                {
                    campus_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campus", x => x.campus_id);
                });

            migrationBuilder.CreateTable(
                name: "facility_type",
                columns: table => new
                {
                    type_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    DefaultAmenities = table.Column<string>(type: "text", nullable: true),
                    DefaultCapacity = table.Column<int>(type: "integer", nullable: true),
                    TypicalDurationHours = table.Column<int>(type: "integer", nullable: true),
                    IconUrl = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility_type", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "system_settings",
                columns: table => new
                {
                    setting_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    setting_value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_settings", x => x.setting_key);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    student_id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    role_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    is_verify = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Unverified"),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email_verification_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    email_verification_code_expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    password_reset_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    password_reset_code_expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
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
                    facility_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    room_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    floor_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    campus_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    type_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Available"),
                    amenities = table.Column<string>(type: "text", nullable: true),
                    facility_manager_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    max_concurrent_bookings = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
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

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    booking_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    facility_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    purpose = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estimated_attendees = table.Column<int>(type: "integer", nullable: true),
                    special_requirements = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pending_Approval"),
                    approved_by = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    approved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejection_reason = table.Column<string>(type: "text", nullable: true),
                    check_in_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    check_in_note = table.Column<string>(type: "text", nullable: true),
                    check_in_images = table.Column<string>(type: "text", nullable: true),
                    check_out_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    check_out_note = table.Column<string>(type: "text", nullable: true),
                    check_out_images = table.Column<string>(type: "text", nullable: true),
                    is_used = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cancelled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cancellation_reason = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK_booking_facility_facility_id",
                        column: x => x.facility_id,
                        principalTable: "facility",
                        principalColumn: "facility_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_booking_user_approved_by",
                        column: x => x.approved_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_booking_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "booking_feedback",
                columns: table => new
                {
                    feedback_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    booking_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    report_issue = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    issue_description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    resolved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_feedback", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_booking_feedback_booking_booking_id",
                        column: x => x.booking_id,
                        principalTable: "booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_feedback_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    notification_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Unread"),
                    booking_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    feedback_id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    read_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId1 = table.Column<string>(type: "character varying(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_notification_booking_booking_id",
                        column: x => x.booking_id,
                        principalTable: "booking",
                        principalColumn: "booking_id");
                    table.ForeignKey(
                        name: "FK_notification_booking_feedback_feedback_id",
                        column: x => x.feedback_id,
                        principalTable: "booking_feedback",
                        principalColumn: "feedback_id");
                    table.ForeignKey(
                        name: "FK_notification_user_UserId1",
                        column: x => x.UserId1,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_notification_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "campus",
                columns: new[] { "campus_id", "address", "created_at", "email", "image_url", "name", "phone_number", "updated_at" },
                values: new object[,]
                {
                    { "C0001", "Lô E2a-7, Đường D1, Khu Công nghệ cao, P.Long Thạnh Mỹ, Tp. Thủ Đức, TP.HCM", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(6384), "daihocfpt@fpt.edu.vn", null, "FPTU HCM Campus", "028 7300 5588", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(6385) },
                    { "C0002", "Số 1 Lưu Hữu Phước, Đông Hoà, Dĩ An, TP.HCM", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(6387), "nvhsv@fpt.edu.vn", null, "Nhà Văn Hóa Sinh Viên", "028 7300 5589", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(6388) }
                });

            migrationBuilder.InsertData(
                table: "facility_type",
                columns: new[] { "type_id", "created_at", "DefaultAmenities", "DefaultCapacity", "description", "IconUrl", "name", "TypicalDurationHours", "updated_at" },
                values: new object[,]
                {
                    { "FT0001", new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4647), null, null, "Phòng học lý thuyết", null, "Phòng học", null, new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4648) },
                    { "FT0002", new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4685), null, null, "Phòng họp", null, "Phòng họp", null, new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4686) },
                    { "FT0003", new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4687), null, null, "Phòng máy tính", null, "Phòng máy tính", null, new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4688) },
                    { "FT0004", new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4689), null, null, "Sân thể thao", null, "Sân thể thao", null, new DateTime(2026, 2, 5, 1, 54, 16, 571, DateTimeKind.Utc).AddTicks(4690) }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "role_id", "created_at", "role_name", "updated_at" },
                values: new object[,]
                {
                    { "RL0001", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3934), "Sinh viên", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3934) },
                    { "RL0002", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3936), "Giảng viên", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3937) },
                    { "RL0003", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3938), "Quản trị viên cơ sở vật chất", new DateTime(2026, 2, 5, 1, 54, 16, 570, DateTimeKind.Utc).AddTicks(3938) }
                });

            migrationBuilder.InsertData(
                table: "facility",
                columns: new[] { "facility_id", "amenities", "campus_id", "capacity", "created_at", "description", "facility_manager_id", "floor_number", "max_concurrent_bookings", "name", "room_number", "type_id", "updated_at" },
                values: new object[,]
                {
                    { "F00001", null, "C0001", 40, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5775), "Phòng học lý thuyết", null, "1", 1, "Phòng A101", "A101", "FT0001", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5775) },
                    { "F00002", null, "C0001", 40, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5779), "Phòng học lý thuyết", null, "1", 1, "Phòng A102", "A102", "FT0001", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5780) },
                    { "F00003", null, "C0001", 15, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5783), "Phòng họp nhỏ", null, "2", 1, "Phòng họp B201", "B201", "FT0002", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5783) },
                    { "F00004", null, "C0001", 25, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5785), "Phòng họp vừa", null, "2", 1, "Phòng họp B202", "B202", "FT0002", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5786) },
                    { "F00005", null, "C0001", 50, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5788), "Phòng máy 50 máy", null, "3", 1, "Lab máy tính C301", "C301", "FT0003", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5788) },
                    { "F00006", null, "C0001", 50, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5791), "Phòng máy 50 máy", null, "3", 1, "Lab máy tính C302", "C302", "FT0003", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5798) },
                    { "F00007", null, "C0001", 100, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5800), "Sân bóng rổ ngoài trời", null, "0", 2, "Sân bóng rổ", "Court1", "FT0004", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5800) },
                    { "F00008", null, "C0001", 80, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5803), "4 sân cầu lông", null, "0", 4, "Sân cầu lông", "Court2", "FT0004", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5803) },
                    { "F00011", null, "C0002", 30, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5898), "Phòng sinh hoạt câu lạc bộ", null, "1", 1, "Phòng N101", "N101", "FT0001", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5900) },
                    { "F00012", null, "C0002", 30, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5903), "Phòng sinh hoạt câu lạc bộ", null, "1", 1, "Phòng N102", "N102", "FT0001", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5903) },
                    { "F00013", null, "C0002", 15, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5906), "Phòng họp Ban chủ nhiệm", null, "2", 1, "Phòng họp NVHSV 201", "N201", "FT0002", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5907) },
                    { "F00014", null, "C0002", 25, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5909), "Phòng họp CLB", null, "2", 1, "Phòng họp NVHSV 202", "N202", "FT0002", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5909) },
                    { "F00015", null, "C0002", 20, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5912), "Phòng sản xuất nội dung", null, "3", 1, "Phòng Media NVHSV", "N301", "FT0003", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5912) },
                    { "F00016", null, "C0002", 25, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5914), "Phòng tập nhạc, ca hát", null, "3", 1, "Phòng Âm nhạc", "N302", "FT0003", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5915) },
                    { "F00017", null, "C0002", 200, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5917), "Sân khấu tổ chức sự kiện", null, "0", 1, "Sân khấu ngoài trời", "Stage1", "FT0004", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5917) },
                    { "F00018", null, "C0002", 50, new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5919), "Khu vực nướng ngoài trời", null, "0", 2, "Khu vực BBQ", "BBQ1", "FT0004", new DateTime(2026, 2, 5, 1, 54, 16, 935, DateTimeKind.Utc).AddTicks(5920) }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "user_id", "avatar_url", "created_at", "email", "email_verification_code", "email_verification_code_expiry", "full_name", "is_verify", "last_login", "password", "password_reset_code", "password_reset_code_expiry", "phone_number", "role_id", "student_id", "updated_at", "user_name" },
                values: new object[,]
                {
                    { "U00001", null, new DateTime(2026, 2, 5, 1, 54, 16, 689, DateTimeKind.Utc).AddTicks(2659), "student@fpt.edu.vn", null, null, "Nguyễn Văn A", "Unverified", null, "$2a$11$Gd1Ho7C98hbEHDavyPYR.uFXlJq5Qto1Z/Yia0QHSgNGrBaiv9d2.", null, null, null, "RL0001", null, new DateTime(2026, 2, 5, 1, 54, 16, 689, DateTimeKind.Utc).AddTicks(2666), "studentA" },
                    { "U00002", null, new DateTime(2026, 2, 5, 1, 54, 16, 811, DateTimeKind.Utc).AddTicks(1627), "lecturer@fe.edu.vn", null, null, "Trần Thị B", "Unverified", null, "$2a$11$RPLPn4CKfWWlqIhvuzkPmu.tbAdG6rTRTYm7Xsr1rPWaMMNSDoHdO", null, null, null, "RL0002", null, new DateTime(2026, 2, 5, 1, 54, 16, 811, DateTimeKind.Utc).AddTicks(1633), "lecturerB" },
                    { "U00003", null, new DateTime(2026, 2, 5, 1, 54, 16, 932, DateTimeKind.Utc).AddTicks(9572), "admin@fpt.edu.vn", null, null, "Quản trị viên hệ thống", "Unverified", null, "$2a$11$DJehRtzWQhjVZ3U78nol1eSJ9yf342xyAvx/GsyLSpzfXI4yX78Ha", null, null, null, "RL0003", null, new DateTime(2026, 2, 5, 1, 54, 16, 932, DateTimeKind.Utc).AddTicks(9581), "admin" }
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
                name: "IX_booking_feedback_booking_id",
                table: "booking_feedback",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_feedback_user_id",
                table: "booking_feedback",
                column: "user_id");

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
                name: "IX_notification_booking_id",
                table: "notification",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_created_at",
                table: "notification",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_notification_feedback_id",
                table: "notification",
                column: "feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_status",
                table: "notification",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_notification_user_id",
                table: "notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_UserId1",
                table: "notification",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_system_settings_setting_key",
                table: "system_settings",
                column: "setting_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_name",
                table: "user",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "system_settings");

            migrationBuilder.DropTable(
                name: "booking_feedback");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "facility");

            migrationBuilder.DropTable(
                name: "campus");

            migrationBuilder.DropTable(
                name: "facility_type");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
