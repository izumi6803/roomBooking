using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityManagerToFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "facility_manager_id",
                table: "facility",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(8398), new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(8399) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(8402), new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(8403) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8624), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8624) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8630), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8634), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8635) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8639), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8639) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8643), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8654), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8655) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8658), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8659) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8663), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8663) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8667), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8667) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8671), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8671) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8836), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8837) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8841), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8842) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8845), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8850), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8854), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8855) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8858), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8859) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8863), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8864) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8868), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8868) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8872), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8872) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "facility_manager_id", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8876), null, new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8877) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9284), new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9284) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9287), new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9288) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9292), new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9293) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9295), new DateTime(2025, 12, 5, 1, 32, 33, 559, DateTimeKind.Utc).AddTicks(9295) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5364), new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5365) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5368), new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5369) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5371), new DateTime(2025, 12, 5, 1, 32, 33, 558, DateTimeKind.Utc).AddTicks(5372) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 722, DateTimeKind.Utc).AddTicks(1912), "$2a$11$n81b0e/706O3HpOq2iJ4Ke/HqwO50dqfXtfK0dDC2fQXg00A/fHXC", new DateTime(2025, 12, 5, 1, 32, 33, 722, DateTimeKind.Utc).AddTicks(1916) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 33, 882, DateTimeKind.Utc).AddTicks(932), "$2a$11$INssS/mhNd2W5mdF7HgfeObLQO6OvQvGiLXUJ8Da3q2hxJ5DOUFIK", new DateTime(2025, 12, 5, 1, 32, 33, 882, DateTimeKind.Utc).AddTicks(939) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 43, DateTimeKind.Utc).AddTicks(3065), "$2a$11$pVlqwi9OUyOy8RvsYOdaJOsxOm8JhI4ZQLob9nq5XUFcmMAEDxf2a", new DateTime(2025, 12, 5, 1, 32, 34, 43, DateTimeKind.Utc).AddTicks(3071) });

            migrationBuilder.CreateIndex(
                name: "IX_facility_facility_manager_id",
                table: "facility",
                column: "facility_manager_id");

            migrationBuilder.AddForeignKey(
                name: "FK_facility_user_facility_manager_id",
                table: "facility",
                column: "facility_manager_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_facility_user_facility_manager_id",
                table: "facility");

            migrationBuilder.DropIndex(
                name: "IX_facility_facility_manager_id",
                table: "facility");

            migrationBuilder.DropColumn(
                name: "facility_manager_id",
                table: "facility");

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
    }
}
