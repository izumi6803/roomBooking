using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCampusImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "campus",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "image_url", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 31, DateTimeKind.Utc).AddTicks(1367), null, new DateTime(2025, 12, 22, 7, 34, 34, 31, DateTimeKind.Utc).AddTicks(1368) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "image_url", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 31, DateTimeKind.Utc).AddTicks(1373), null, new DateTime(2025, 12, 22, 7, 34, 34, 31, DateTimeKind.Utc).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4098), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4098) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4103), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4104) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4108), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4108) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4112), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4113) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4116), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4126) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4134), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4135) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4138), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4139) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4151), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4152) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4155), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4156) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4159), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4163), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4164) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4167), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4168) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4172) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4175), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4175) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4179), new DateTime(2025, 12, 22, 7, 34, 34, 545, DateTimeKind.Utc).AddTicks(4179) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8473), new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8475) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8479), new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8480) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8483), new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8484) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8487), new DateTime(2025, 12, 22, 7, 34, 34, 32, DateTimeKind.Utc).AddTicks(8488) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6392), new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6393) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6397), new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6401), new DateTime(2025, 12, 22, 7, 34, 34, 30, DateTimeKind.Utc).AddTicks(6401) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 201, DateTimeKind.Utc).AddTicks(7002), "$2a$11$zBMA/TG1XML7dbh9siisr.4iLRaQwhGeiH.KS1t/vZFdM1fNFSqNi", new DateTime(2025, 12, 22, 7, 34, 34, 201, DateTimeKind.Utc).AddTicks(7009) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 369, DateTimeKind.Utc).AddTicks(4458), "$2a$11$fd9nnfzhF.AQggTIGGEFzOETBDZynD6OnTC96.1gXwxOkTqtJXZyW", new DateTime(2025, 12, 22, 7, 34, 34, 369, DateTimeKind.Utc).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 22, 7, 34, 34, 541, DateTimeKind.Utc).AddTicks(6587), "$2a$11$kKI6TEYQPaDxV/HdviXr.uRICxy1aQlVyMxERNNX9OQmh3Ggw0soi", new DateTime(2025, 12, 22, 7, 34, 34, 541, DateTimeKind.Utc).AddTicks(6593) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "campus");

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
    }
}
