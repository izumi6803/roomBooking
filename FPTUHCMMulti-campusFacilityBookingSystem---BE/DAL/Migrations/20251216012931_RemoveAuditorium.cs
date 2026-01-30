using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuditorium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009");

            migrationBuilder.DeleteData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010");

            migrationBuilder.DeleteData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019");

            migrationBuilder.DeleteData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020");

            migrationBuilder.DeleteData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(9499), new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(9499) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(9503), new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(9503) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4115), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4115) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4119), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4120) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4123), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4124) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4127), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4127) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4133), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4139) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4142), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4142) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4145), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4145) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4241), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4241) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4244), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4245) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4248), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4248) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4251), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4251) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4254), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4255) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4376), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4377) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4382), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4383) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4386), new DateTime(2025, 12, 16, 1, 29, 28, 575, DateTimeKind.Utc).AddTicks(4386) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9329), new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9331) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9334), new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9334) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9336), new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9339), new DateTime(2025, 12, 16, 1, 29, 28, 98, DateTimeKind.Utc).AddTicks(9339) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5853), new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5853) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5856), new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5856) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5858), new DateTime(2025, 12, 16, 1, 29, 28, 97, DateTimeKind.Utc).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 256, DateTimeKind.Utc).AddTicks(9562), "$2a$11$7VNWW0oTSyxBfY.ir3zmee/D2GULaWU/Bbx4TsXdPyYW7eZrJCxKW", new DateTime(2025, 12, 16, 1, 29, 28, 256, DateTimeKind.Utc).AddTicks(9569) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 414, DateTimeKind.Utc).AddTicks(4865), "$2a$11$c8K8Bu0NL/DTmS68SqQg1OZ70MGgDIprB1bhA3Jt5uEyWqe6ewlru", new DateTime(2025, 12, 16, 1, 29, 28, 414, DateTimeKind.Utc).AddTicks(4869) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 1, 29, 28, 572, DateTimeKind.Utc).AddTicks(3200), "$2a$11$m3G6cCVs2XX0DscEQVVxaeNXrly1S/ayhEyR3HiKMKXu6u3dko1a6", new DateTime(2025, 12, 16, 1, 29, 28, 572, DateTimeKind.Utc).AddTicks(3204) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "facility_type",
                columns: new[] { "type_id", "created_at", "DefaultAmenities", "DefaultCapacity", "description", "IconUrl", "name", "status", "TypicalDurationHours", "updated_at" },
                values: new object[] { "FT0005", new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2140), null, null, "Hội trường", null, "Hội trường", "Active", null, new DateTime(2025, 12, 16, 0, 2, 45, 134, DateTimeKind.Utc).AddTicks(2141) });

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
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 368, DateTimeKind.Utc).AddTicks(7535), "$2a$11$b5tWRv.2m/tv/yFlJUidzeKiht4wUUt3Ef1.bPDHAzT0e7ksBvDbC", new DateTime(2025, 12, 16, 0, 2, 45, 368, DateTimeKind.Utc).AddTicks(7539) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 559, DateTimeKind.Utc).AddTicks(6599), "$2a$11$t2jqLjktzBcK.p7SSF0YxeOdiRFd3ncRfjx0wTLDdrpt/6gw3jEZO", new DateTime(2025, 12, 16, 0, 2, 45, 559, DateTimeKind.Utc).AddTicks(6603) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 16, 0, 2, 45, 756, DateTimeKind.Utc).AddTicks(242), "$2a$11$pu2zZh3XZQhmwoXbOP5xw.PsE2NKABU/Hi2w1iobTEYDDnEmo/jiW", new DateTime(2025, 12, 16, 0, 2, 45, 756, DateTimeKind.Utc).AddTicks(245) });

            migrationBuilder.InsertData(
                table: "facility",
                columns: new[] { "facility_id", "amenities", "campus_id", "capacity", "created_at", "description", "facility_manager_id", "floor_number", "max_concurrent_bookings", "name", "room_number", "status", "type_id", "updated_at" },
                values: new object[,]
                {
                    { "F00009", null, "C0001", 500, new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5620), "Hội trường lớn", null, "1", 1, "Hội trường A", "HallA", "Available", "FT0005", new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5621) },
                    { "F00010", null, "C0001", 200, new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5624), "Hội trường nhỏ", null, "1", 1, "Hội trường B", "HallB", "Available", "FT0005", new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5624) },
                    { "F00019", null, "C0002", 500, new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5727), "Hội trường tổ chức sự kiện lớn", null, "1", 1, "Hội trường NVHSV", "HallNVH", "Available", "FT0005", new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5727) },
                    { "F00020", null, "C0002", 80, new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5730), "Phòng tổ chức workshop, training", null, "2", 1, "Phòng Workshop", "Workshop1", "Available", "FT0005", new DateTime(2025, 12, 16, 0, 2, 45, 758, DateTimeKind.Utc).AddTicks(5730) }
                });
        }
    }
}
