using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constants_Constants_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Constants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fathers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Migrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    migration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Migrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Fathers_FatherId",
                        column: x => x.FatherId,
                        principalTable: "Fathers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InMenu = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAjax = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pages_PageCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pages_Pages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Constants_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Constants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackStudentTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackStudentTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackStudentTransfers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackStudentTransfers_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackStudentTransfers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackStudentTransfers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Comment", "Icon", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, null, null, "العملة", null },
                    { 5, null, null, "الجنس", null },
                    { 8, null, null, "نوع المكان المقصود", null },
                    { 12, null, null, "نوع المرفق", null }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "الادارة", true },
                    { 2, "إدارة العملاء", true },
                    { 3, "إدارة الخدمات", true },
                    { 4, "المالية", true },
                    { 5, "البريد", false },
                    { 6, "المصروفات", false },
                    { 7, "الخدمات", false },
                    { 8, "التقارير", false }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "أفغانستاني", "Afghan" },
                    { 2, "ألباني", "Albanian" },
                    { 3, "آلاندي", "Aland Islander" },
                    { 4, "جزائري", "Algerian" },
                    { 5, "أمريكي سامواني", "American Samoan" },
                    { 6, "أندوري", "Andorran" },
                    { 7, "أنقولي", "Angolan" },
                    { 8, "أنغويلي", "Anguillan" },
                    { 9, "أنتاركتيكي", "Antarctican" },
                    { 10, "بربودي", "Antiguan" },
                    { 11, "أرجنتيني", "Argentinian" },
                    { 12, "أرميني", "Armenian" },
                    { 13, "أوروبهيني", "Aruban" },
                    { 14, "أسترالي", "Australian" },
                    { 15, "نمساوي", "Austrian" },
                    { 16, "أذربيجاني", "Azerbaijani" },
                    { 17, "باهاميسي", "Bahamian" },
                    { 18, "بحريني", "Bahraini" },
                    { 19, "بنغلاديشي", "Bangladeshi" },
                    { 20, "بربادوسي", "Barbadian" },
                    { 21, "روسي", "Belarusian" },
                    { 22, "بلجيكي", "Belgian" },
                    { 23, "بيليزي", "Belizean" },
                    { 24, "بنيني", "Beninese" },
                    { 25, "سان بارتيلمي", "Saint Barthelmian" },
                    { 26, "برمودي", "Bermudan" },
                    { 27, "بوتاني", "Bhutanese" },
                    { 28, "بوليفي", "Bolivian" },
                    { 29, "بوسني/هرسكي", "Bosnian / Herzegovinian" },
                    { 30, "بوتسواني", "Botswanan" },
                    { 31, "بوفيهي", "Bouvetian" },
                    { 32, "برازيلي", "Brazilian" },
                    { 33, "إقليم المحيط الهندي البريطاني", "British Indian Ocean Territory" },
                    { 34, "بروني", "Bruneian" },
                    { 35, "بلغاري", "Bulgarian" },
                    { 36, "بوركيني", "Burkinabe" },
                    { 37, "بورونيدي", "Burundian" },
                    { 38, "كمبودي", "Cambodian" },
                    { 39, "كاميروني", "Cameroonian" },
                    { 40, "كندي", "Canadian" },
                    { 41, "الرأس الأخضر", "Cape Verdean" },
                    { 42, "كايماني", "Caymanian" },
                    { 43, "أفريقي", "Central African" },
                    { 44, "تشادي", "Chadian" },
                    { 45, "شيلي", "Chilean" },
                    { 46, "صيني", "Chinese" },
                    { 47, "جزيرة عيد الميلاد", "Christmas Islander" },
                    { 48, "جزر كوكوس", "Cocos Islander" },
                    { 49, "كولومبي", "Colombian" },
                    { 50, "جزر القمر", "Comorian" },
                    { 51, "كونغي", "Congolese" },
                    { 52, "جزر كوك", "Cook Islander" },
                    { 53, "كوستاريكي", "Costa Rican" },
                    { 54, "كوراتي", "Croatian" },
                    { 55, "كوبي", "Cuban" },
                    { 56, "قبرصي", "Cypriot" },
                    { 57, "كوراساوي", "Curacian" },
                    { 58, "تشيكي", "Czech" },
                    { 59, "دنماركي", "Danish" },
                    { 60, "جيبوتي", "Djiboutian" },
                    { 61, "دومينيكي", "Dominican" },
                    { 62, "دومينيكي", "Dominican" },
                    { 63, "إكوادوري", "Ecuadorian" },
                    { 64, "مصري", "Egyptian" },
                    { 65, "سلفادوري", "Salvadoran" },
                    { 66, "غيني", "Equatorial Guinean" },
                    { 67, "إريتيري", "Eritrean" },
                    { 68, "استوني", "Estonian" },
                    { 69, "أثيوبي", "Ethiopian" },
                    { 70, "فوكلاندي", "Falkland Islander" },
                    { 71, "جزر فارو", "Faroese" },
                    { 72, "فيجي", "Fijian" },
                    { 73, "فنلندي", "Finnish" },
                    { 74, "فرنسي", "French" },
                    { 75, "غويانا الفرنسية", "French Guianese" },
                    { 76, "بولينيزيي", "French Polynesian" },
                    { 77, "أراض فرنسية جنوبية وأنتارتيكية", "French" },
                    { 78, "غابوني", "Gabonese" },
                    { 79, "غامبي", "Gambian" },
                    { 80, "جيورجي", "Georgian" },
                    { 81, "ألماني", "German" },
                    { 82, "غاني", "Ghanaian" },
                    { 83, "جبل طارق", "Gibraltar" },
                    { 84, "غيرنزي", "Guernsian" },
                    { 85, "يوناني", "Greek" },
                    { 86, "جرينلاندي", "Greenlandic" },
                    { 87, "غرينادي", "Grenadian" },
                    { 88, "جزر جوادلوب", "Guadeloupe" },
                    { 89, "جوامي", "Guamanian" },
                    { 90, "غواتيمالي", "Guatemalan" },
                    { 91, "غيني", "Guinean" },
                    { 92, "غيني", "Guinea-Bissauan" },
                    { 93, "غياني", "Guyanese" },
                    { 94, "هايتي", "Haitian" },
                    { 95, "جزيرة هيرد وجزر ماكدونالد", "Heard and Mc Donald Islanders" },
                    { 96, "هندوراسي", "Honduran" },
                    { 97, "هونغ كونغي", "Hongkongese" },
                    { 98, "مجري", "Hungarian" },
                    { 99, "آيسلندي", "Icelandic" },
                    { 100, "هندي", "Indian" },
                    { 101, "ماني", "Manx" },
                    { 102, "أندونيسيي", "Indonesian" },
                    { 103, "إيراني", "Iranian" },
                    { 104, "عراقي", "Iraqi" },
                    { 105, "إيرلندي", "Irish" },
                    { 106, "إسرائيلي", "Israeli" },
                    { 107, "إيطالي", "Italian" },
                    { 108, "ساحل العاج", "Ivory Coastian" },
                    { 109, "جيرزي", "Jersian" },
                    { 110, "جمايكي", "Jamaican" },
                    { 111, "ياباني", "Japanese" },
                    { 112, "أردني", "Jordanian" },
                    { 113, "كازاخستاني", "Kazakh" },
                    { 114, "كيني", "Kenyan" },
                    { 115, "كيريباتي", "I-Kiribati" },
                    { 116, "كوري", "North Korean" },
                    { 117, "كوري", "South Korean" },
                    { 118, "كوسيفي", "Kosovar" },
                    { 119, "كويتي", "Kuwaiti" },
                    { 120, "قيرغيزستاني", "Kyrgyzstani" },
                    { 121, "لاوسي", "Laotian" },
                    { 122, "لاتيفي", "Latvian" },
                    { 123, "لبناني", "Lebanese" },
                    { 124, "ليوسيتي", "Basotho" },
                    { 125, "ليبيري", "Liberian" },
                    { 126, "ليبي", "Libyan" },
                    { 127, "ليختنشتيني", "Liechtenstein" },
                    { 128, "لتوانيي", "Lithuanian" },
                    { 129, "لوكسمبورغي", "Luxembourger" },
                    { 130, "سريلانكي", "Sri Lankian" },
                    { 131, "ماكاوي", "Macanese" },
                    { 132, "مقدوني", "Macedonian" },
                    { 133, "مدغشقري", "Malagasy" },
                    { 134, "مالاوي", "Malawian" },
                    { 135, "ماليزي", "Malaysian" },
                    { 136, "مالديفي", "Maldivian" },
                    { 137, "مالي", "Malian" },
                    { 138, "مالطي", "Maltese" },
                    { 139, "مارشالي", "Marshallese" },
                    { 140, "مارتينيكي", "Martiniquais" },
                    { 141, "موريتانيي", "Mauritanian" },
                    { 142, "موريشيوسي", "Mauritian" },
                    { 143, "مايوتي", "Mahoran" },
                    { 144, "مكسيكي", "Mexican" },
                    { 145, "مايكرونيزيي", "Micronesian" },
                    { 146, "مولديفي", "Moldovan" },
                    { 147, "مونيكي", "Monacan" },
                    { 148, "منغولي", "Mongolian" },
                    { 149, "الجبل الأسود", "Montenegrin" },
                    { 150, "مونتسيراتي", "Montserratian" },
                    { 151, "مغربي", "Moroccan" },
                    { 152, "موزمبيقي", "Mozambican" },
                    { 153, "ميانماري", "Myanmarian" },
                    { 154, "ناميبي", "Namibian" },
                    { 155, "نوري", "Nauruan" },
                    { 156, "نيبالي", "Nepalese" },
                    { 157, "هولندي", "Dutch" },
                    { 158, "هولندي", "Dutch Antilier" },
                    { 159, "كاليدونيا", "New Caledonian" },
                    { 160, "نيوزيلندي", "New Zealander" },
                    { 161, "نيكاراجوي", "Nicaraguan" },
                    { 162, "نيجيري", "Nigerien" },
                    { 163, "نيجيري", "Nigerian" },
                    { 164, "ني", "Niuean" },
                    { 165, "نورفوليكي", "Norfolk Islander" },
                    { 166, "ماريني", "Northern Marianan" },
                    { 167, "نرويجي", "Norwegian" },
                    { 168, "عماني", "Omani" },
                    { 169, "باكستاني", "Pakistani" },
                    { 170, "بالاوي", "Palauan" },
                    { 171, "فلسطيني", "Palestinian" },
                    { 172, "بنمي", "Panamanian" },
                    { 173, "بابوي", "Papua New Guinean" },
                    { 174, "بارغاوي", "Paraguayan" },
                    { 175, "بيري", "Peruvian" },
                    { 176, "فلبيني", "Filipino" },
                    { 177, "بيتكيرني", "Pitcairn Islander" },
                    { 178, "بولندي", "Polish" },
                    { 179, "برتغالي", "Portuguese" },
                    { 180, "بورتي", "Puerto Rican" },
                    { 181, "قطري", "Qatari" },
                    { 182, "ريونيوني", "Reunionese" },
                    { 183, "روماني", "Romanian" },
                    { 184, "روسي", "Russian" },
                    { 185, "رواندا", "Rwandan" },
                    { 186, "سانت كيتس ونيفس", "Kittitian/Nevisian" },
                    { 187, "ساينت مارتني فرنسي", "St. Martian(French)" },
                    { 188, "ساينت مارتني هولندي", "St. Martian(Dutch)" },
                    { 189, "سان بيير وميكلوني", "St. Pierre and Miquelon" },
                    { 190, "سانت فنسنت وجزر غرينادين", "Saint Vincent and the Grenadines" },
                    { 191, "ساموي", "Samoan" },
                    { 192, "ماريني", "Sammarinese" },
                    { 193, "ساو تومي وبرينسيبي", "Sao Tomean" },
                    { 194, "سعودي", "Saudi Arabian" },
                    { 195, "سنغالي", "Senegalese" },
                    { 196, "صربي", "Serbian" },
                    { 197, "سيشيلي", "Seychellois" },
                    { 198, "سيراليوني", "Sierra Leonean" },
                    { 199, "سنغافوري", "Singaporean" },
                    { 200, "سولفاكي", "Slovak" },
                    { 201, "سولفيني", "Slovenian" },
                    { 202, "جزر سليمان", "Solomon Island" },
                    { 203, "صومالي", "Somali" },
                    { 204, "أفريقي", "South African" },
                    { 205, "لمنطقة القطبية الجنوبية", "South Georgia and the South Sandwich" },
                    { 206, "سوادني جنوبي", "South Sudanese" },
                    { 207, "إسباني", "Spanish" },
                    { 208, "هيلاني", "St. Helenian" },
                    { 209, "سوداني", "Sudanese" },
                    { 210, "سورينامي", "Surinamese" },
                    { 211, "سفالبارد ويان ماين", "Svalbardian/Jan Mayenian" },
                    { 212, "سوازيلندي", "Swazi" },
                    { 213, "سويدي", "Swedish" },
                    { 214, "سويسري", "Swiss" },
                    { 215, "سوري", "Syrian" },
                    { 216, "تايواني", "Taiwanese" },
                    { 217, "طاجيكستاني", "Tajikistani" },
                    { 218, "تنزانيي", "Tanzanian" },
                    { 219, "تايلندي", "Thai" },
                    { 220, "تيموري", "Timor-Lestian" },
                    { 221, "توغي", "Togolese" },
                    { 222, "توكيلاوي", "Tokelaian" },
                    { 223, "تونغي", "Tongan" },
                    { 224, "ترينيداد وتوباغو", "Trinidadian/Tobagonian" },
                    { 225, "تونسي", "Tunisian" },
                    { 226, "تركي", "Turkish" },
                    { 227, "تركمانستاني", "Turkmen" },
                    { 228, "جزر توركس وكايكوس", "Turks and Caicos Islands" },
                    { 229, "توفالي", "Tuvaluan" },
                    { 230, "أوغندي", "Ugandan" },
                    { 231, "أوكراني", "Ukrainian" },
                    { 232, "إماراتي", "Emirati" },
                    { 233, "بريطاني", "British" },
                    { 234, "أمريكي", "American" },
                    { 235, "أمريكي", "US Minor Outlying Islander" },
                    { 236, "أورغواي", "Uruguayan" },
                    { 237, "أوزباكستاني", "Uzbek" },
                    { 238, "فانواتي", "Vanuatuan" },
                    { 239, "فنزويلي", "Venezuelan" },
                    { 240, "فيتنامي", "Vietnamese" },
                    { 241, "أمريكي", "American Virgin Islander" },
                    { 242, "فاتيكاني", "Vatican" },
                    { 243, "فوتوني", "Wallisian/Futunan" },
                    { 244, "صحراوي", "Sahrawian" },
                    { 245, "يمني", "Yemeni" },
                    { 246, "زامبياني", "Zambian" },
                    { 247, "زمبابوي", "Zimbabwean" }
                });

            migrationBuilder.InsertData(
                table: "PageCategories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Header" },
                    { 2, false, "Page" },
                    { 3, false, "Tool" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "IsDeleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 8, 18, 13, 42, 49, 289, DateTimeKind.Local).AddTicks(5623), null, false, "مدير النظام", null, null },
                    { 2, null, new DateTime(2026, 8, 18, 13, 42, 49, 291, DateTimeKind.Local).AddTicks(6875), null, false, "مستخدم", null, null }
                });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Comment", "Icon", "Name", "ParentId" },
                values: new object[,]
                {
                    { 2, null, null, "دولار", 1 },
                    { 3, null, null, "دينار", 1 },
                    { 4, null, null, "شيكل", 1 },
                    { 6, null, null, "ذكر", 5 },
                    { 7, null, null, "أنثى", 5 },
                    { 9, null, null, "دولة", 8 },
                    { 10, null, null, "مدينة", 8 },
                    { 11, null, null, "محافظة", 8 },
                    { 13, null, null, "جواز سفر", 12 },
                    { 14, null, null, "هوية", 12 },
                    { 15, null, null, "شهادة ثانوية عامة", 12 },
                    { 16, null, null, "شهادة دبلوم", 12 },
                    { 17, null, null, "شهادة بكالوريس", 12 },
                    { 18, null, null, "شهادة ماجستير", 12 }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId" },
                values: new object[] { 1, 1, null, false, false, false, false, null, null, "الاب", "Parent Page", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "DeletedBy", "Email", "EmailConfirmed", "GenderId", "IsActive", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedBy", "UpdatedOn", "UserName", "UserTypeId" },
                values: new object[] { "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3", 0, "default_avatar.png", "77d8d173-7d86-400b-975f-7a4ecbc830e7", null, new DateTime(2026, 8, 18, 13, 42, 49, 291, DateTimeKind.Local).AddTicks(7721), null, "admin@Academia.com", false, 2, true, false, false, null, "Academia Admin", null, "ADMIN@Academia.COM", "AQAAAAIAAYagAAAAEPi7zHV998Yw+puOvGLldoK5gN3MTy25WArmlj9/7DgKvYvvrYhZNbkBjpwVu+oqFQ==", "", false, "e8df2c7c-8596-4e02-b255-d08f6a975e5a", false, null, null, "admin@Academia.com", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId" },
                values: new object[,]
                {
                    { 2, 2, "bi bi-house-fill", true, true, false, false, "Home/Index", null, "الرئيسية", "Home", 1 },
                    { 3, 1, "bi bi-list-ul", true, true, false, false, null, 1, "الإدارة", "Management", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "PageId", "UserTypeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId" },
                values: new object[,]
                {
                    { 4, 1, "bi bi-people", true, true, false, false, null, 1, "إدارة المستخدمين", "Users Management", 3 },
                    { 8, 2, "bi bi-geo-alt-fill", true, true, false, false, "Destination/Index", 1, "المحافظات و المدن", "Governorates and Cities", 3 },
                    { 9, 2, "bi bi-view-list", true, true, false, false, "Management/Modules", 1, "وحدات النظام", "Governorates and Cities", 3 },
                    { 10, 2, "bi bi-window-stack", true, true, false, false, "Page/Index", 1, "الصفحات", "Pages", 3 },
                    { 11, 2, "fa fa-anchor", true, true, false, false, "Constant/Index", 1, "الثوابت", "Constants", 3 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "PageId", "UserTypeId" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 3, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId" },
                values: new object[,]
                {
                    { 5, 2, "bi bi-person-fill", true, true, false, false, "User/Index", 1, "المستخدمين", "Users", 4 },
                    { 6, 2, "bi bi-people", true, true, false, false, "UserType/Index", 1, "أنواع المستخدمين", "User Types", 4 },
                    { 7, 2, "bi bi-check-lg", true, true, false, false, "UserPermission/Index", 1, "صلاحيات المستخدم", "User Permissions", 4 },
                    { 26, 3, null, false, true, true, false, "Destination/GetAll", 1, "عرض بيانات جدول المحافظات والمدن", "Display Governorates and Cities DateTable", 8 },
                    { 27, 3, null, false, true, true, false, "Destination/CreateEditModal", 1, "عرض واجهة إضافة تعديل وجهة", "Display create Edit Destination page", 8 },
                    { 28, 3, null, false, true, true, false, "Destination/CreateEdit", 1, "إضافة تعديل وجهة", "create Edit Destination", 8 },
                    { 29, 3, null, false, true, true, false, "Destination/Delete", 1, "حذف وجهة", "Delete Destination", 8 },
                    { 30, 3, null, false, true, true, false, "Management/SwitchStatus", 1, "تبديل حالات وحدات النظام", "Switching states of system Modules", 9 },
                    { 31, 3, null, false, true, true, false, "Page/GetAll", 1, "عرض بيانات جدول الصفحات", "Display Pages DataTable", 10 },
                    { 32, 3, null, false, true, true, false, "Page/CreateEditModal", 1, "عرض واجهة إضافة  تعديل صفحة", "Display Create Edit Page interface", 10 },
                    { 33, 3, null, false, true, true, false, "Page/CreateEdit", 1, "إضافة تعديل صفحة", "Create Edit Page", 10 },
                    { 34, 3, null, false, true, true, false, "Page/Delete", 1, "حذف صفحة", "Delete Page", 10 },
                    { 35, 3, null, false, true, true, false, "Constant/GetAll", 1, "عرض بيانات جدول الثوابت", "Display Constant DataTable", 11 },
                    { 36, 3, null, false, true, true, false, "Constant/CreateEditModal", 1, "عرض واجهة إضافة تعديل ثوابت", "Display Create Edit Constant Page", 11 },
                    { 37, 3, null, false, true, true, false, "Constant/CreateEdit", 1, "إضافة تعديل ثوابت", "Create Edit Constant", 11 },
                    { 38, 3, null, false, true, true, false, "Constant/Delete", 1, "حذف ثابت", "Delete Constant", 11 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "PageId", "UserTypeId" },
                values: new object[,]
                {
                    { 4, 4, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId" },
                values: new object[,]
                {
                    { 12, 3, null, false, true, true, false, "User/GetAll", 1, "عرض بيانات جدول المستخدمين", "Display User DataTable", 5 },
                    { 13, 3, null, false, true, true, false, "User/CreateEditModal", 1, "اظهار واجهة اضافة  تعديل مستخدم", "Display Create Edit User Page", 5 },
                    { 14, 3, null, false, true, true, false, "User/CreateEdit", 1, "اضافة تعديل مستخدم", "Create Edit User", 5 },
                    { 15, 3, null, false, true, true, false, "User/Delete", 1, "حذف مستخدم", "Delete User", 5 },
                    { 16, 3, null, false, true, true, false, "User/MyProfileModal", 1, "عرض واجهة ملفي الشخصي", "Display My Profile Page", 5 },
                    { 17, 3, null, false, true, true, false, "User/MyProfile", 1, "تعديل ملفي الشخصي", "Update My Profile", 5 },
                    { 18, 3, null, false, true, true, false, "User/ChangePasswordModal", 1, "عرض واجهة تغير كلمة المرور", "Display Change Password Page", 5 },
                    { 19, 3, null, false, true, true, false, "User/ChangePassword", 1, "تغير كلمة المرور", "ChangePassword", 5 },
                    { 20, 3, null, false, true, true, false, "UserType/GetAll", 1, "عرض بيانات جدول انواع المستخدين", "Display User Type DateTable", 6 },
                    { 21, 3, null, false, true, true, false, "UserType/CreateEditModal", 1, "عرض واجهة اضافة  تعديل نوع المستخدم", "Display Create Edit User Type page", 6 },
                    { 22, 3, null, false, true, true, false, "UserType/CreateEdit", 1, "اضافة تعديل نوع مستخدم", "Create Edit User Type ", 6 },
                    { 23, 3, null, false, true, true, false, "UserType/Delete", 1, "حذف نوع مستخدم", "Delete User Type ", 6 },
                    { 24, 3, null, false, true, true, false, "UserPermission/GetUserTypePermissions", 1, "عرض صلاحيات نوع المستخدم", "display User Type Permissions", 7 },
                    { 25, 3, null, false, true, true, false, "UserPermission/SavePermissions", 1, "حفظ صلاحيات نوع المستخدم", "Save User Type Permissions", 7 }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "PageId", "UserTypeId" },
                values: new object[,]
                {
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 26, 26, 1 },
                    { 27, 27, 1 },
                    { 28, 28, 1 },
                    { 29, 29, 1 },
                    { 30, 30, 1 },
                    { 31, 31, 1 },
                    { 32, 32, 1 },
                    { 33, 33, 1 },
                    { 34, 34, 1 },
                    { 35, 35, 1 },
                    { 36, 36, 1 },
                    { 37, 37, 1 },
                    { 38, 38, 1 },
                    { 12, 12, 1 },
                    { 13, 13, 1 },
                    { 14, 14, 1 },
                    { 15, 15, 1 },
                    { 16, 16, 1 },
                    { 17, 17, 1 },
                    { 18, 18, 1 },
                    { 19, 19, 1 },
                    { 20, 20, 1 },
                    { 21, 21, 1 },
                    { 22, 22, 1 },
                    { 23, 23, 1 },
                    { 24, 24, 1 },
                    { 25, 25, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniqueEmail",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniquePhoneNo",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Constants_ParentId",
                table: "Constants",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GradeId",
                table: "Groups",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CategoryId",
                table: "Pages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ModuleId",
                table: "Pages",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ParentId",
                table: "Pages",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FatherId",
                table: "Students",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                table: "Students",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GradeId",
                table: "Teachers",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackStudentTransfers_GradeId",
                table: "TrackStudentTransfers",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackStudentTransfers_StudentId",
                table: "TrackStudentTransfers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackStudentTransfers_TeacherId",
                table: "TrackStudentTransfers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackStudentTransfers_UserId",
                table: "TrackStudentTransfers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PageId",
                table: "UserPermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserTypeId",
                table: "UserPermissions",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_UniqueName",
                table: "UserTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Migrations");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "TrackStudentTransfers");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "Fathers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "PageCategories");
        }
    }
}
