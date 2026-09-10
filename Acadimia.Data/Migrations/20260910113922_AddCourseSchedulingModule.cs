using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSchedulingModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MeetingInstructions",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingPlatform",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingUrl",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Lessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseEndDate",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseStartDate",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultLessonDurationMinutes",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxStudents",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fathers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppNumber",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxStudents",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupScheduleDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupScheduleDays_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "SecurityStamp" },
                values: new object[] { "db1a740f-2ed6-4774-a16f-7c4b9768a4d8", new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(9868), "ffffea2c-c3c1-400b-b2d2-b43f96f269a7" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 64, DateTimeKind.Local).AddTicks(1021));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6247));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6264));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6267));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6270));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6274));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 66, DateTimeKind.Local).AddTicks(6277));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(1945));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2461));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2471));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2474));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2477));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2493));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2496));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2533));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2547));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2550));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2556));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2561));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2563));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2569));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(2571));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(8292));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 14, 39, 21, 68, DateTimeKind.Local).AddTicks(9073));

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId_ScheduledDate",
                table: "Lessons",
                columns: new[] { "CourseId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId_ScheduledDate",
                table: "Lessons",
                columns: new[] { "GroupId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScheduleDays_GroupId_DayOfWeek",
                table: "GroupScheduleDays",
                columns: new[] { "GroupId", "DayOfWeek" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupScheduleDays");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CourseId_ScheduledDate",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_GroupId_ScheduledDate",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CourseId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "MeetingInstructions",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "MeetingPlatform",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "MeetingUrl",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CourseEndDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CourseStartDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DefaultLessonDurationMinutes",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "MaxStudents",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "WhatsAppNumber",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MaxStudents",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "SecurityStamp" },
                values: new object[] { "9e64ace4-4f21-4fe8-8e5d-8ee7296afffb", new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8864), "78a56435-7d8f-44c3-9f57-1f08ef11f0e9" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 12, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8582));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8595));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8599));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8608));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8614));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8620));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8623));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1302));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1842));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1857));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1868));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1887));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1893));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1899));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1905));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1910));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1912));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1915));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1918));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1921));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1928));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1931));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8102));

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons",
                column: "GroupId");
        }
    }
}
