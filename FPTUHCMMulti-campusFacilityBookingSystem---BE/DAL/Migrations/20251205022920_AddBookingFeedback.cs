using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booking_feedback",
                columns: table => new
                {
                    feedback_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    report_issue = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    issue_description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    resolved_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_feedback", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_booking_feedback_booking_booking_id",
                        column: x => x.booking_id,
                        principalTable: "booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_feedback_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(201), new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(202) });

            migrationBuilder.UpdateData(
                table: "campus",
                keyColumn: "campus_id",
                keyValue: "C0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(208), new DateTime(2025, 12, 5, 2, 29, 19, 222, DateTimeKind.Utc).AddTicks(208) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5819), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5825), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5828), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5829) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5832), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5832) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5835), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5841) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5844), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5847), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5851), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5854), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5857), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5858) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5914), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5914) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5917), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5921) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5924), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5924) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5927), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5927) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5974), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5975) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5978), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5979) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5982), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5982) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5985), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5985) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5988), new DateTime(2025, 12, 5, 2, 29, 19, 775, DateTimeKind.Utc).AddTicks(5989) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1436), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1439) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1444), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1445) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1449), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1454), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1455) });

            migrationBuilder.UpdateData(
                table: "facility_type",
                keyColumn: "type_id",
                keyValue: "FT0005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1459), new DateTime(2025, 12, 5, 2, 29, 19, 224, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0001",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6034), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6035) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6038), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6039) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "role_id",
                keyValue: "RL0003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6042), new DateTime(2025, 12, 5, 2, 29, 19, 221, DateTimeKind.Utc).AddTicks(6043) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00001",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 428, DateTimeKind.Utc).AddTicks(7996), "$2a$11$kr3DdpcQ0D63OOFK7q5hg.OhNkwl4qENuRP6Lf6LLoWYh91BDg6tW", new DateTime(2025, 12, 5, 2, 29, 19, 428, DateTimeKind.Utc).AddTicks(8001) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00002",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 599, DateTimeKind.Utc).AddTicks(6991), "$2a$11$VAy6dzSb1LL0Y5JdyAsCP.CqvkP6oS508gS5mLt6IzFD9F2eiL6h6", new DateTime(2025, 12, 5, 2, 29, 19, 599, DateTimeKind.Utc).AddTicks(6997) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "user_id",
                keyValue: "U00003",
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 2, 29, 19, 772, DateTimeKind.Utc).AddTicks(6098), "$2a$11$32HwEljfLBTiW644PJ0KJ.t96nZH.cgcwf1vgrRuJtth2qS2bg4um", new DateTime(2025, 12, 5, 2, 29, 19, 772, DateTimeKind.Utc).AddTicks(6104) });

            migrationBuilder.CreateIndex(
                name: "IX_booking_feedback_booking_id_user_id",
                table: "booking_feedback",
                columns: new[] { "booking_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_feedback_user_id",
                table: "booking_feedback",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_feedback");

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
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8624), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8624) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00002",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00003",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8634), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8635) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00004",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8639), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8639) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00005",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8643), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00006",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8654), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8655) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00007",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8658), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8659) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00008",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8663), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8663) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00009",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8667), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8667) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00010",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8671), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8671) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00011",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8836), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8837) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00012",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8841), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8842) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00013",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8845), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00014",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8850), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00015",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8854), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8855) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00016",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8858), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8859) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00017",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8863), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8864) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00018",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8868), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8868) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00019",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8872), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8872) });

            migrationBuilder.UpdateData(
                table: "facility",
                keyColumn: "facility_id",
                keyValue: "F00020",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8876), new DateTime(2025, 12, 5, 1, 32, 34, 45, DateTimeKind.Utc).AddTicks(8877) });

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
        }
    }
}
