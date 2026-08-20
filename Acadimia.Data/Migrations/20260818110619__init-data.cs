using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class _initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04dfc155-9708-48d8-aaf2-d1158d345e3d", new DateTime(2026, 8, 18, 14, 6, 18, 553, DateTimeKind.Local).AddTicks(1302), "0594727849Ziad#", "498da5f1-869b-4722-9ed9-6c1836ef3c22" });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 18, 14, 6, 18, 550, DateTimeKind.Local).AddTicks(2482));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 18, 14, 6, 18, 553, DateTimeKind.Local).AddTicks(94));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77d8d173-7d86-400b-975f-7a4ecbc830e7", new DateTime(2026, 8, 18, 13, 42, 49, 291, DateTimeKind.Local).AddTicks(7721), "AQAAAAIAAYagAAAAEPi7zHV998Yw+puOvGLldoK5gN3MTy25WArmlj9/7DgKvYvvrYhZNbkBjpwVu+oqFQ==", "e8df2c7c-8596-4e02-b255-d08f6a975e5a" });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 18, 13, 42, 49, 289, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 18, 13, 42, 49, 291, DateTimeKind.Local).AddTicks(6875));
        }
    }
}
