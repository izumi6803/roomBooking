using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserCampusRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_campus_campus_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_campus_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "campus_id",
                table: "user");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4199), new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "address", "created_at", "email", "name", "phone_number", "updated_at" },
                values: new object[] { "Số 1 Lưu Hữu Phước, Đông Hoà, Dĩ An, TP.HCM", new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4207), "nvhsv@fpt.edu.vn", "Nhà Văn Hóa Sinh Viên", "028 7300 5589", new DateTime(2025, 12, 5, 0, 46, 1, 367, DateTimeKind.Utc).AddTicks(4208) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(950), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(950) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(954), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(955) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(958), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(958) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(962), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(962) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(965), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(973) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(976), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(976) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1008), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1009) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1012), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1012) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1015), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1016) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1019), new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1019) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 30, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1086), "Phòng sinh hoạt câu lạc bộ", "Phòng N101", "N101", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1087) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 30, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1090), "Phòng sinh hoạt câu lạc bộ", "Phòng N102", "N102", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1090) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 15, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1093), "Phòng họp Ban chủ nhiệm", "Phòng họp NVHSV 201", "N201", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1094) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 25, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1096), "Phòng họp CLB", "Phòng họp NVHSV 202", "N202", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1097) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 20, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1100), "Phòng sản xuất nội dung", "Phòng Media NVHSV", "N301", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1100) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 25, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1103), "Phòng tập nhạc, ca hát", "Phòng Âm nhạc", "N302", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1103) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "capacity", "created_at", "description", "max_concurrent_bookings", "name", "room_number", "updated_at" },
                values: new object[] { 200, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1106), "Sân khấu tổ chức sự kiện", 1, "Sân khấu ngoài trời", "Stage1", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1107) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 50, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1109), "Khu vực nướng ngoài trời", "Khu vực BBQ", "BBQ1", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1110) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 500, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1113), "Hội trường tổ chức sự kiện lớn", "Hội trường NVHSV", "HallNVH", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1113) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 80, new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1116), "Phòng tổ chức workshop, training", "Phòng Workshop", "Workshop1", new DateTime(2025, 12, 5, 0, 46, 1, 926, DateTimeKind.Utc).AddTicks(1116) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "campus_id",
                table: "user",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8827), new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8828) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "address", "created_at", "email", "name", "phone_number", "updated_at" },
                values: new object[] { "Khu đô thị Khoa học và Giáo dục Quy Hòa, Phường Ghềnh Ráng, TP. Quy Nhơn, Bình Định", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8833), "daihocfpt.qn@fpt.edu.vn", "FPTU Quy Nhơn Campus", "0256 7300 999", new DateTime(2025, 12, 5, 0, 9, 17, 680, DateTimeKind.Utc).AddTicks(8834) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1945), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1946) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1952), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1952) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1956), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1957) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1961), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1965), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1975), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1975) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1979), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1984), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(1984) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2028), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2029) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2033), new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2033) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 35, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2115), "Phòng học lý thuyết", "Phòng QN101", "QN101", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2116) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 35, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2119), "Phòng học lý thuyết", "Phòng QN102", "QN102", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 12, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2124), "Phòng họp nhỏ", "Phòng họp QN201", "QN201", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2125) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 20, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2128), "Phòng họp vừa", "Phòng họp QN202", "QN202", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2129) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2133), "Phòng máy 40 máy", "Lab máy tính QN301", "QN301", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2133) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2137), "Phòng máy 40 máy", "Lab máy tính QN302", "QN302", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2137) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "capacity", "created_at", "description", "max_concurrent_bookings", "name", "room_number", "updated_at" },
                values: new object[] { 50, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2141), "Sân bóng đá 5 người", 2, "Sân bóng đá", "Court1", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2141) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 40, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2145), "2 sân tennis", "Sân tennis", "Court2", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2146) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 400, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2149), "Hội trường chính", "Hội trường QN", "HallQN", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "capacity", "created_at", "description", "name", "room_number", "updated_at" },
                values: new object[] { 100, new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2153), "Phòng hội thảo", "Phòng đa năng QN", "MultiQN", new DateTime(2025, 12, 5, 0, 9, 18, 324, DateTimeKind.Utc).AddTicks(2154) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9499), new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9503), new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9504) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9507), new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9507) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9510), new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9510) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9513), new DateTime(2025, 12, 5, 0, 9, 17, 681, DateTimeKind.Utc).AddTicks(9513) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8829), new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8830) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8834), new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8835) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8837), new DateTime(2025, 12, 5, 0, 9, 17, 676, DateTimeKind.Utc).AddTicks(8838) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "campus_id", "created_at", "password", "updated_at" },
                values: new object[] { "C0001", new DateTime(2025, 12, 5, 0, 9, 17, 907, DateTimeKind.Utc).AddTicks(6718), "$2a$11$ZZJd00NzYPM9jhH5PpKtCOJ4d4aR64wuuKjWiQKMJxxHFoQZYbkWq", new DateTime(2025, 12, 5, 0, 9, 17, 907, DateTimeKind.Utc).AddTicks(6723) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "campus_id", "created_at", "password", "updated_at" },
                values: new object[] { "C0001", new DateTime(2025, 12, 5, 0, 9, 18, 112, DateTimeKind.Utc).AddTicks(4329), "$2a$11$9uDyDC0uIXxTIyHfyS0Pbu.DkQm.EKWMriuZZgZkK6/.2qyYOuY.a", new DateTime(2025, 12, 5, 0, 9, 18, 112, DateTimeKind.Utc).AddTicks(4336) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "campus_id", "created_at", "password", "updated_at" },
                values: new object[] { "C0001", new DateTime(2025, 12, 5, 0, 9, 18, 321, DateTimeKind.Utc).AddTicks(7761), "$2a$11$3O.yiPJ7fOCo9wlrUFfZ8./r2AnqYqF67uBBed5rl1LNl1BHI/G3u", new DateTime(2025, 12, 5, 0, 9, 18, 321, DateTimeKind.Utc).AddTicks(7765) });

            migrationBuilder.CreateIndex(
                name: "IX_user_campus_id",
                table: "user",
                column: "campus_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_campus_campus_id",
                table: "user",
                column: "campus_id",
                principalTable: "campus",
                principalColumn: "campus_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
