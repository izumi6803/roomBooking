using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingFeedbackToOnePerBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_booking_feedback_booking_id_user_id",
                table: "booking_feedback");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 689, DateTimeKind.Utc).AddTicks(1009), new DateTime(2025, 12, 5, 2, 56, 30, 689, DateTimeKind.Utc).AddTicks(1010) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 689, DateTimeKind.Utc).AddTicks(1015), new DateTime(2025, 12, 5, 2, 56, 30, 689, DateTimeKind.Utc).AddTicks(1016) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2742), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2742) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2747), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2747) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2751), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2751) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2754), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2755) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2758), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2765) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2768), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2768) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2771), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2772) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2775), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2775) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2778), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2778) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2781), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2782) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2791) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2794), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2794) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2797), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2797) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2803), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2804) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2806), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2807) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2810), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2813), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2813) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2816), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2817) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2819), new DateTime(2025, 12, 5, 2, 56, 31, 193, DateTimeKind.Utc).AddTicks(2820) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7361), new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7366), new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7367) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7371), new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7372) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7375), new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7375) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7379), new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7379) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6165), new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6166) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6170), new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6171) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 870, DateTimeKind.Utc).AddTicks(6733), "$2a$11$6TdKeQhf5h9V3ZK0AekLYOVphYLa1nxiOjgMy5rl3f/mwKqw2qEva", new DateTime(2025, 12, 5, 2, 56, 30, 870, DateTimeKind.Utc).AddTicks(6738) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 29, DateTimeKind.Utc).AddTicks(2241), "$2a$11$zsdVI3fVIJOsvh1.iUv9dOu7NABTTfhJpGKAfOPYJxToTNh24ntBy", new DateTime(2025, 12, 5, 2, 56, 31, 29, DateTimeKind.Utc).AddTicks(2245) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 191, DateTimeKind.Utc).AddTicks(7488), "$2a$11$0iH4d1WLRUQ7shdgiprY9eTCPnIrGTDqicss6bkxk1Yzgenz8Ag8K", new DateTime(2025, 12, 5, 2, 56, 31, 191, DateTimeKind.Utc).AddTicks(7494) });

            migrationBuilder.CreateIndex(
                name: "IX_booking_feedback_booking_id",
                table: "booking_feedback",
                column: "booking_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_booking_feedback_booking_id",
                table: "booking_feedback");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(201), new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(202) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(208), new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(208) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5819), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5825), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5828), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5829) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5832), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5832) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5835), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5841) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5844), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5847), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5851), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5854), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5857), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5858) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5914), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5914) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5917), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5921) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5924), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5924) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5927), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5927) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5974), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5975) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5978), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5979) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5982), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5982) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5985), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5985) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5988), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5989) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1436), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1439) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1444), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1445) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1449), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1454), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1455) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1459), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6034), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6035) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6038), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6039) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6042), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6043) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 428, DateTimeKind.Utc).AddTicks(7996), "$2a$11$kr3DdpcQ0D63OOFK7q5hg.OhNkwl4qENuRP6Lf6LLoWYh91BDg6tW", new DateTime(2025, 12, 5, 2, 29, 19, 428, DateTimeKind.Utc).AddTicks(8001) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 599, DateTimeKind.Utc).AddTicks(6991), "$2a$11$VAy6dzSb1LL0Y5JdyAsCP.CqvkP6oS508gS5mLt6IzFD9F2eiL6h6", new DateTime(2025, 12, 5, 2, 29, 19, 599, DateTimeKind.Utc).AddTicks(6997) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 772, DateTimeKind.Utc).AddTicks(6098), "$2a$11$32HwEljfLBTiW644PJ0KJ.t96nZH.cgcwf1vgrRuJtth2qS2bg4um", new DateTime(2025, 12, 5, 2, 29, 19, 772, DateTimeKind.Utc).AddTicks(6104) });

            migrationBuilder.CreateIndex(
                name: "IX_booking_feedback_booking_id_user_id",
                table: "booking_feedback",
                columns: new[] { "booking_id", "user_id" },
                unique: true);
        }
    }
}
