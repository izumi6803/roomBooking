using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "student_id",
                table: "user",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(8356), new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(8357) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(8364), new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5585), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5585) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5589), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5593), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5594) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5597), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5597) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5600), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5607) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5610), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5613), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5614) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5617), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5617) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5620), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5621) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5624), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5624) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5700), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5703), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5704) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5707), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5707) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5711) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5714), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5714) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5717), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5717) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5720), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5721) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5724), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5724) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5727), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5727) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2118), new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2119) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2125), new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2126) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2130), new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2135), new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2136) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2140), new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2141) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2462), new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2463) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2466), new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2466) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2468), new DateTime(2025, 12, 16, 0, 2, 45, 131, DateTimeKind.Utc).AddTicks(2469) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "student_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 368, DateTimeKind.Utc).AddTicks(7535), "$2a$11$b5tWRv.2m/tv/yFlJUidzeKiht4wUUt3Ef1.bPDHAzT0e7ksBvDbC", null, new DateTime(2025, 12, 16, 0, 2, 45, 368, DateTimeKind.Utc).AddTicks(7539) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "student_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 559, DateTimeKind.Utc).AddTicks(6599), "$2a$11$t2jqLjktzBcK.p7SSF0YxeOdiRFd3ncRfjx0wTLDdrpt/6gw3jEZO", null, new DateTime(2025, 12, 16, 0, 2, 45, 559, DateTimeKind.Utc).AddTicks(6603) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "student_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 756, DateTimeKind.Utc).AddTicks(242), "$2a$11$pu2zZh3XZQhmwoXbOP5xw.PsE2NKABU/Hi2w1iobTEYDDnEmo/jiW", null, new DateTime(2025, 12, 16, 0, 2, 45, 756, DateTimeKind.Utc).AddTicks(245) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "student_id",
                table: "user");

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
    }
}
