using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameAndPhoneToFather : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade_id",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Pages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParentStudentLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    RelationTypeId = table.Column<int>(type: "int", nullable: true),
                    IsPrimaryContact = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentStudentLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentStudentLinks_AspNetUsers_ParentUserId",
                        column: x => x.ParentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentStudentLinks_Constants_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "Constants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParentStudentLinks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "SecurityStamp" },
                values: new object[] { "9e64ace4-4f21-4fe8-8e5d-8ee7296afffb", new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8864), "78a56435-7d8f-44c3-9f57-1f08ef11f0e9" });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Comment", "Icon", "Name", "ParentId" },
                values: new object[] { 20, null, null, "صلة القرابة", null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 12, DateTimeKind.Local).AddTicks(5361), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(6605), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8582), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8595), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8599), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8608), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8611), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8614), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8617), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8620), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 14, DateTimeKind.Local).AddTicks(8623), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1302), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1842), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1853), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1857), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1868), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1874), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1887), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1890), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1893), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1897), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1899), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1902), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1905), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1907), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1910), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1912), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1915), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1918), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1921), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1923), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1926), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1928), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1931), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1934), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1936), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1939), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedBy", "CreatedOn", "DeletedBy", "UpdatedBy", "UpdatedOn" },
                values: new object[] { null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(1941), null, null, null });

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

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8099), null, false, "الطالب", null, null },
                    { 4, null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8101), null, false, "المعلم", null, null },
                    { 5, null, new DateTime(2026, 9, 8, 14, 15, 55, 16, DateTimeKind.Local).AddTicks(8102), null, false, "ولي الامر", null, null }
                });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Comment", "Icon", "Name", "ParentId" },
                values: new object[,]
                {
                    { 21, null, null, "اب", 20 },
                    { 22, null, null, "ام", 20 },
                    { 23, null, null, "ابن", 20 },
                    { 24, null, null, "بنت", 20 },
                    { 25, null, null, "زوج", 20 },
                    { 26, null, null, "زوجة", 20 },
                    { 27, null, null, "وصي", 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentStudentLinks_ParentUserId_StudentId",
                table: "ParentStudentLinks",
                columns: new[] { "ParentUserId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentStudentLinks_RelationTypeId",
                table: "ParentStudentLinks",
                column: "RelationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentStudentLinks_StudentId",
                table: "ParentStudentLinks",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentStudentLinks");

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Constants",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Pages");

            migrationBuilder.AddColumn<int>(
                name: "Grade_id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "SecurityStamp" },
                values: new object[] { "ced9d2cc-9a9f-4b58-a6cc-d97344c6ef0d", new DateTime(2026, 8, 20, 16, 10, 22, 576, DateTimeKind.Local).AddTicks(7127), "76363698-052e-496f-af7c-5e53f22b4482" });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 20, 16, 10, 22, 574, DateTimeKind.Local).AddTicks(3013));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 8, 20, 16, 10, 22, 576, DateTimeKind.Local).AddTicks(6156));
        }
    }
}
