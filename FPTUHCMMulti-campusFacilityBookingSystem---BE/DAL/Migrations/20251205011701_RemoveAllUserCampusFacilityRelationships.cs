using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAllUserCampusFacilityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_campus_user_facility_manager_id",
                table: "campus");

            migrationBuilder.DropForeignKey(
                name: "FK_facility_user_facility_manager_id",
                table: "facility");

            migrationBuilder.DropIndex(
                name: "IX_facility_facility_manager_id",
                table: "facility");

            migrationBuilder.DropIndex(
                name: "IX_campus_facility_manager_id",
                table: "campus");

            migrationBuilder.DropColumn(
                name: "facility_manager_id",
                table: "facility");

            migrationBuilder.DropColumn(
                name: "facility_manager_id",
                table: "campus");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 957, DateTimeKind.Utc).AddTicks(1749), new DateTime(2025, 12, 5, 1, 16, 59, 957, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 957, DateTimeKind.Utc).AddTicks(1753), new DateTime(2025, 12, 5, 1, 16, 59, 957, DateTimeKind.Utc).AddTicks(1754) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4783), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4784) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4788), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4789) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4793), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4796), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4797) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4800), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4803), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4809) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4812), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4813) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4816), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4816) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4819), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4823), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4823) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4921) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4924), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4924) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4927), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4928) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4931), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4932) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4935), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4935) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4938), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4939) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4942), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4942) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4945), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4945) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4948), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4949) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4952), new DateTime(2025, 12, 5, 1, 17, 0, 628, DateTimeKind.Utc).AddTicks(4952) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5291) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5294), new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5294) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5296), new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5297) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5298), new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5299) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5301), new DateTime(2025, 12, 5, 1, 16, 59, 958, DateTimeKind.Utc).AddTicks(5302) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9037), new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9038) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9042), new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9042) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9044), new DateTime(2025, 12, 5, 1, 16, 59, 956, DateTimeKind.Utc).AddTicks(9045) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 180, DateTimeKind.Utc).AddTicks(5285), "$2a$11$yJUAytvcgJLojG9F9OJLl.8Zj21ilGor.sev8dc1STfAs1Cxs4Zxa", new DateTime(2025, 12, 5, 1, 17, 0, 180, DateTimeKind.Utc).AddTicks(5292) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 397, DateTimeKind.Utc).AddTicks(3377), "$2a$11$EhRu8OyzruCIMGTGdZn6xe.52xsJHEWIa5OtXLiFcIbbF9SA14lhu", new DateTime(2025, 12, 5, 1, 17, 0, 397, DateTimeKind.Utc).AddTicks(3384) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 17, 0, 624, DateTimeKind.Utc).AddTicks(992), "$2a$11$wF.szSTRiTSYouOJoEJ4qOCpoRsbpTLYE7qpI5Z59.hP1.NO9D0UW", new DateTime(2025, 12, 5, 1, 17, 0, 624, DateTimeKind.Utc).AddTicks(998) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "facility_manager_id",
                table: "facility",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facility_manager_id",
                table: "campus",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4199), null, new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4207), null, new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4208) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(950), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(950) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(954), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(955) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(958), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(958) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(962), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(962) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(965), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(973) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(976), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(976) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1008), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1009) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1012), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1012) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1015), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1016) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1019), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1019) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1086), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1087) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1090), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1090) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1093), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1094) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1096), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1097) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1100), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1100) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1103), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1103) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1106), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1107) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1109), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1110) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1113), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1113) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1116), null, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1116) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1265), new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1268) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1274), new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1275) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1282) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1285), new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1286) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 12, 5, 0, 46, 1, 370, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3700), new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3702) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3708), new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3714), new DateTime(2025, 12, 5, 0, 46, 1, 366, DateTimeKind.Utc).AddTicks(3715) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 601, DateTimeKind.Utc).AddTicks(2846), "$2a$11$OWTk9V5vXqZwPwOXUgNWOOOJs2oVtNbB2VS5FF9JILp2mGKlIfRIK", new DateTime(2025, 12, 5, 0, 46, 1, 601, DateTimeKind.Utc).AddTicks(2851) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 763, DateTimeKind.Utc).AddTicks(4051), "$2a$11$n.Onw/H0rpKtB/3CUzsN.eNDZLEctDjuIO/oh.gmG.zARzzGtT2/2", new DateTime(2025, 12, 5, 0, 46, 1, 763, DateTimeKind.Utc).AddTicks(4056) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 923, DateTimeKind.Utc).AddTicks(9773), "$2a$11$/snMZ84bBmZxAh0.N0YFY./PhdB/IBKaPUCM52ZmXYXogOEY/sbw6", new DateTime(2025, 12, 5, 0, 46, 1, 923, DateTimeKind.Utc).AddTicks(9777) });

            migrationBuilder.CreateIndex(
                name: "IX_facility_facility_manager_id",
                table: "facility",
                column: "facility_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_campus_facility_manager_id",
                table: "campus",
                column: "facility_manager_id");

            migrationBuilder.AddForeignKey(
                name: "FK_campus_user_facility_manager_id",
                table: "campus",
                column: "facility_manager_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_facility_user_facility_manager_id",
                table: "facility",
                column: "facility_manager_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
