using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentLocationAndLessonRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "SecurityStamp" },
                values: new object[] { "ed0042ae-f2ee-4f35-a39d-bd68c666f034", new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(8567), "e92908da-cb3f-4ed5-aaa0-e922beef81b8" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 743, DateTimeKind.Local).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8003));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8021));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8033));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8036));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8039));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8043));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 745, DateTimeKind.Local).AddTicks(8045));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1538));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1575));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1588));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1592));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1601));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1605));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1611));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1616));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1631));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1634));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1638));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1642));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1658));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1663));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1666));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1668));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1671));

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(7808));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 9, 10, 15, 36, 40, 747, DateTimeKind.Local).AddTicks(7809));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Lessons");

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
        }
    }
}
