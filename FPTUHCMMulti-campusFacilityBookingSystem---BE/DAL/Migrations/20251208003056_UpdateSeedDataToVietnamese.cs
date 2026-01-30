using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataToVietnamese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(4679), new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(4679) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(4683), new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(4683) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5151) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5156), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5157) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5161) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5164), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5164) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5167), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5173) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5176), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5177) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5183), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5184) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5187), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5187) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5191) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5269), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5269) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5272), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5273) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5276), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5276) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5279), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5279) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5282), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5283) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5286), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5286) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5289), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5289) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5292), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5293) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5296), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5296) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5299), new DateTime(2025, 12, 8, 0, 30, 52, 683, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3100), "Phòng học", new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3102) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3104), "Phòng họp", new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3105) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3107), "Phòng máy tính", new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3107) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3109), "Sân thể thao", new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3112), "Hội trường", new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3112) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1121), "Sinh viên", new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1122) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1125), "Giảng viên", new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1125) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1127), "Quản trị viên cơ sở vật chất", new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1128) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 336, DateTimeKind.Utc).AddTicks(5669), "$2a$11$x.27BmhZUhJvilUQup61y.KeBAhuo7cmrvM/eQ4nMAbzqajL0dDqu", new DateTime(2025, 12, 8, 0, 30, 52, 336, DateTimeKind.Utc).AddTicks(5673) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 495, DateTimeKind.Utc).AddTicks(538), "$2a$11$YRGQH4P8hC5e3LBiEo3l8utB8UC8.kyG1Pby7Ikz3QOQVUN/h/63K", new DateTime(2025, 12, 8, 0, 30, 52, 495, DateTimeKind.Utc).AddTicks(541) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "full_name", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 680, DateTimeKind.Utc).AddTicks(2049), "Quản trị viên hệ thống", "$2a$11$RnPdq1kFx9D9OZbTqRP3XeHc2ItOAq2A7O3Mba8GShBwGQ4pJEcvu", new DateTime(2025, 12, 8, 0, 30, 52, 680, DateTimeKind.Utc).AddTicks(2055) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7361), "Classroom", new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7366), "Meeting Room", new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7367) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7371), "Computer Lab", new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7372) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7375), "Sports Court", new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7375) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7379), "Auditorium", new DateTime(2025, 12, 5, 2, 56, 30, 690, DateTimeKind.Utc).AddTicks(7379) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6160), "Student", new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6165), "Lecturer", new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6166) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "role_name", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6170), "Facility_Admin", new DateTime(2025, 12, 5, 2, 56, 30, 688, DateTimeKind.Utc).AddTicks(6171) });

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
                columns: new[] { "created_at", "full_name", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 56, 31, 191, DateTimeKind.Utc).AddTicks(7488), "Admin System", "$2a$11$0iH4d1WLRUQ7shdgiprY9eTCPnIrGTDqicss6bkxk1Yzgenz8Ag8K", new DateTime(2025, 12, 5, 2, 56, 31, 191, DateTimeKind.Utc).AddTicks(7494) });
        }
    }
}
