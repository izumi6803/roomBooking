using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_settings",
                columns: table => new
                {
                    setting_key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    setting_value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_settings", x => x.setting_key);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_system_settings_setting_key",
                table: "system_settings",
                column: "setting_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_settings");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(9785), new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(9786) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(9788), new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(9788) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5263), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5263) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5267), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5268) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5271), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5271) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5274), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5274) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5277), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5277) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5286) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5293), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5293) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5296), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5296) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5299), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5299) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5306), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5307) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5309), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5309) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5312), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5312) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5315), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5315) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5318), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5319) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5321), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5322) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5324), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5325) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5328), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5328) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5331) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5333), new DateTime(2025, 12, 12, 1, 37, 52, 216, DateTimeKind.Utc).AddTicks(5334) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1044), new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1046) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1049), new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1050) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1051), new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1052) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1053), new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1054) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1055), new DateTime(2025, 12, 12, 1, 37, 51, 740, DateTimeKind.Utc).AddTicks(1055) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6389), new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6391) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6393), new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6394) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6395), new DateTime(2025, 12, 12, 1, 37, 51, 738, DateTimeKind.Utc).AddTicks(6396) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 51, 890, DateTimeKind.Utc).AddTicks(7798), "$2a$11$uCpIrFhTatgU1FTDnJ//wu6pjIK.V0xVYay5BUm3CqBJBNSsaiMA.", new DateTime(2025, 12, 12, 1, 37, 51, 890, DateTimeKind.Utc).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 46, DateTimeKind.Utc).AddTicks(3022), "$2a$11$lCeUNIwNJbYs7Oi90VidG.rhrzUGnHlwb0qriCuabvNf7kt19C1Jy", new DateTime(2025, 12, 12, 1, 37, 52, 46, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 12, 1, 37, 52, 214, DateTimeKind.Utc).AddTicks(3978), "$2a$11$H5kAP5.cCy5eojvKtPeqY.lmgVfZ0nQViKby9/KFTE.OA19bODdcm", new DateTime(2025, 12, 12, 1, 37, 52, 214, DateTimeKind.Utc).AddTicks(3987) });
        }
    }
}
