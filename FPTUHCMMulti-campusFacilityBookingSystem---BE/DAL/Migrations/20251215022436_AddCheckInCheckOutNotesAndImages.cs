using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckInCheckOutNotesAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "check_in_images",
                table: "booking",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "check_in_note",
                table: "booking",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "check_out_images",
                table: "booking",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "check_out_note",
                table: "booking",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(4843), new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(4844) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(4847), new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(4847) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3308), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3308) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3312), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3312) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3315), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3315) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3318), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3318) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3357), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3362) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3365), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3365) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3368), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3368) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3371), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3372) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3374), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3375) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3377), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3377) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3433), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3435), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3436) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3438), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3438) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3441), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3441) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3443), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3444) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3446), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3446) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3449), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3449) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3451), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3452) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3454), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3454) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3457), new DateTime(2025, 12, 15, 2, 24, 32, 372, DateTimeKind.Utc).AddTicks(3457) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(7995), new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(7997) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8065), new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8066) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8069), new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8069) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8071), new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8072) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8074), new DateTime(2025, 12, 15, 2, 24, 31, 809, DateTimeKind.Utc).AddTicks(8074) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(722), new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(724) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(727), new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(727) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(729), new DateTime(2025, 12, 15, 2, 24, 31, 808, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 31, 984, DateTimeKind.Utc).AddTicks(5375), "$2a$11$827OXeOf6Auo6vBWnXRzRug/4yOo0dqh8OadHfGvbbJL.AswU1Rj.", new DateTime(2025, 12, 15, 2, 24, 31, 984, DateTimeKind.Utc).AddTicks(5401) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 194, DateTimeKind.Utc).AddTicks(4376), "$2a$11$EdleYTx70W5kHGiAQoy9i.uZGWiIct.zV8uTj5V8EGNr920/b5I5O", new DateTime(2025, 12, 15, 2, 24, 32, 194, DateTimeKind.Utc).AddTicks(4381) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 15, 2, 24, 32, 368, DateTimeKind.Utc).AddTicks(7059), "$2a$11$BIZiMrkis5XiYDsXeL6yt./2Ib7anYd0IiP1cmuJuDk.xQDVrKSzG", new DateTime(2025, 12, 15, 2, 24, 32, 368, DateTimeKind.Utc).AddTicks(7104) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "check_in_images",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "check_in_note",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "check_out_images",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "check_out_note",
                table: "booking");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 872, DateTimeKind.Utc).AddTicks(3793), new DateTime(2025, 12, 12, 2, 44, 2, 872, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 872, DateTimeKind.Utc).AddTicks(3796), new DateTime(2025, 12, 12, 2, 44, 2, 872, DateTimeKind.Utc).AddTicks(3796) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4755), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4756) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4761) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4764), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4764) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4767), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4768) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4778), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4778) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4782), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4782) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4785), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4785) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4881), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4882) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4885), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4885) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4946), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4947) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4953), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4953) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4955), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4956) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4958), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4959) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4961), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4962) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4964), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4965) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4967), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4968) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4971) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4973), new DateTime(2025, 12, 12, 2, 44, 3, 353, DateTimeKind.Utc).AddTicks(4973) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6163), new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6166), new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6167) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6169), new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6169) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6171), new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6171) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6173), new DateTime(2025, 12, 12, 2, 44, 2, 873, DateTimeKind.Utc).AddTicks(6173) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9901), new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9901) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9904), new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9905) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9906), new DateTime(2025, 12, 12, 2, 44, 2, 871, DateTimeKind.Utc).AddTicks(9907) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 31, DateTimeKind.Utc).AddTicks(9645), "$2a$11$UctQtzWsquqhUlpBl5bfxuUQX4/hIrcKRLWsJCW3qx7iEw2WvYFtq", new DateTime(2025, 12, 12, 2, 44, 3, 31, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 203, DateTimeKind.Utc).AddTicks(5953), "$2a$11$g9kulI.kuH9VcJkWZGnDpe8eQFJD7NcSfiNpl5oZi15iNolTAjUIK", new DateTime(2025, 12, 12, 2, 44, 3, 203, DateTimeKind.Utc).AddTicks(5961) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 2, 44, 3, 351, DateTimeKind.Utc).AddTicks(2669), "$2a$11$pPOhtzJJNS7/iDKijzZRwOFSdHla7R.unjDB8cszHkbG8tYMvJydO", new DateTime(2025, 12, 12, 2, 44, 3, 351, DateTimeKind.Utc).AddTicks(2675) });
        }
    }
}
