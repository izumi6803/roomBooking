using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityTypeStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "facility_type",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Active");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "facility_type");

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
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3102) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3104), new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3105) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3107), new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3107) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3109), new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3112), new DateTime(2025, 12, 8, 0, 30, 52, 153, DateTimeKind.Utc).AddTicks(3112) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1121), new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1122) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1125), new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1125) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1127), new DateTime(2025, 12, 8, 0, 30, 52, 152, DateTimeKind.Utc).AddTicks(1128) });

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
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 8, 0, 30, 52, 680, DateTimeKind.Utc).AddTicks(2049), "$2a$11$RnPdq1kFx9D9OZbTqRP3XeHc2ItOAq2A7O3Mba8GShBwGQ4pJEcvu", new DateTime(2025, 12, 8, 0, 30, 52, 680, DateTimeKind.Utc).AddTicks(2055) });
        }
    }
}
