using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDraftStatusFromBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing Draft bookings to Pending_Approval
            migrationBuilder.Sql("UPDATE booking SET status = 'Pending_Approval' WHERE status = 'Draft'");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "booking",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending_Approval",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Draft");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "booking",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Draft",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Pending_Approval");

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
        }
    }
}
