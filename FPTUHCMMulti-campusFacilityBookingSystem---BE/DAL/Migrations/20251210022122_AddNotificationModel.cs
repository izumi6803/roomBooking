using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "user",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    notification_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Unread"),
                    booking_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    feedback_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    read_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(6)", nullable: true)
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

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(7459), new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(7465), new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(7466) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9268), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9269) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9274), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9275) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9344), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9345) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9348), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9359) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9363), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9364) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9367), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9368) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9372), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9372) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9380), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9451) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9455), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9455) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9459), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9459) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9463), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9463) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9467), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9467) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9471), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9471) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9475), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9475) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9479), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9479) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9482), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9483) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9486), new DateTime(2025, 12, 10, 2, 21, 22, 376, DateTimeKind.Utc).AddTicks(9487) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7835), new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7837) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7841) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7844), new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7845) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7848), new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7849) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 12, 10, 2, 21, 21, 900, DateTimeKind.Utc).AddTicks(7911) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(553), new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(554) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(558), new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(559) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(562), new DateTime(2025, 12, 10, 2, 21, 21, 898, DateTimeKind.Utc).AddTicks(563) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 62, DateTimeKind.Utc).AddTicks(9346), "$2a$11$mlzqNVf8XuIw.eibGsBbJeuQb0ZeCtJ77vBc60Fq6.hjNrsi3hcXi", new DateTime(2025, 12, 10, 2, 21, 22, 62, DateTimeKind.Utc).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 218, DateTimeKind.Utc).AddTicks(7151), "$2a$11$H1wfehj8lGSH.s5Eg2IeeuIRd7WCj1iVvchDoxI4BUgVEkxroCTFO", new DateTime(2025, 12, 10, 2, 21, 22, 218, DateTimeKind.Utc).AddTicks(7157) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 10, 2, 21, 22, 374, DateTimeKind.Utc).AddTicks(5306), "$2a$11$jc4YFPVjpTDLT2WFK3s3DezqwDolDoqe1BTjTUbb3k7TRMSaIOjN2", new DateTime(2025, 12, 10, 2, 21, 22, 374, DateTimeKind.Utc).AddTicks(5311) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.AlterColumn<string>(
                name: "avatar_url",
                table: "user",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(3397), new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(3397) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(3399), new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(3400) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8642), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8643) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8646), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8646) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8649), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8652) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8655), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8655) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8657), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8658) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8660), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8667) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8669), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8672), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8673) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8675), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8675) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8678), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8678) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8740), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8741) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8743), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8744) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8746), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8746) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8749), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8750) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8752), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8752) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8755), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8755) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8758), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8758) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8761) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8763), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8763) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8766), new DateTime(2025, 12, 8, 2, 36, 39, 200, DateTimeKind.Utc).AddTicks(8766) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3693), new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3695) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3697), new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3698) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3700), new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3700) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3702), new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3702) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3703), new DateTime(2025, 12, 8, 2, 36, 38, 778, DateTimeKind.Utc).AddTicks(3704) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(331), new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(331) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(333), new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(333) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(335), new DateTime(2025, 12, 8, 2, 36, 38, 777, DateTimeKind.Utc).AddTicks(335) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 38, 916, DateTimeKind.Utc).AddTicks(3866), "$2a$11$16XQo9Nss8bbzO6h53c5duR/VOEhqqMtceGgr/iP3R0GC.OglOHs6", new DateTime(2025, 12, 8, 2, 36, 38, 916, DateTimeKind.Utc).AddTicks(3874) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 53, DateTimeKind.Utc).AddTicks(1945), "$2a$11$eQwWvAihls0UVW2iNqvJR.f6KOhXhR39h5d9GrzftBboxlJGSzj8m", new DateTime(2025, 12, 8, 2, 36, 39, 53, DateTimeKind.Utc).AddTicks(1953) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 2, 36, 39, 198, DateTimeKind.Utc).AddTicks(7923), "$2a$11$qPZWxDSvD.nRNf059lPOqugx4Gd62NtmapUo6mMphMkjB8s6OZpti", new DateTime(2025, 12, 8, 2, 36, 39, 198, DateTimeKind.Utc).AddTicks(7928) });
        }
    }
}
