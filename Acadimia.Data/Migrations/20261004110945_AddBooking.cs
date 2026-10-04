using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acadimia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBooking : Migration
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
                name: "CourseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "PlatformCommissionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformCommissionSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
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
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedEntityId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    ServiceArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportsOnline = table.Column<bool>(type: "bit", nullable: false),
                    SupportsInPerson = table.Column<bool>(type: "bit", nullable: false),
                    HourlyPriceOnline = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HourlyPriceInPerson = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublicForDiscovery = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_Teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalletTopUpRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VerifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTopUpRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTopUpRequests_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTopUpRequests_AspNetUsers_VerifiedBy",
                        column: x => x.VerifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankIBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransferReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalRequests_AspNetUsers_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalRequests_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true),
                    TeachingMode = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StudentNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeliveryType = table.Column<int>(type: "int", nullable: false),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TeachingMode = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAvailabilities_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGradeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGradeLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGradeLevels_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherGradeLevels_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedEntityId = table.Column<int>(type: "int", nullable: true),
                    DecisionBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DecisionOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_AspNetUsers_DecisionBy",
                        column: x => x.DecisionBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingRescheduleRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    OriginalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ProposedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProposedStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DecisionBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRescheduleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRescheduleRequests_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherRatings_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherRatings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherRatings_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    CourseStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultLessonDurationMinutes = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Groups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecordedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_RecordedBy",
                        column: x => x.RecordedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalMarks = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "JoinRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TargetType = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DecisionBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DecisionOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinRequests_AspNetUsers_DecisionBy",
                        column: x => x.DecisionBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinRequests_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MeetingPlatform = table.Column<int>(type: "int", nullable: true),
                    MeetingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ScoreObtained = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResults_AspNetUsers_GradedBy",
                        column: x => x.GradedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamResults_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResults_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    JoinRequestId = table.Column<int>(type: "int", nullable: false),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_JoinRequests_JoinRequestId",
                        column: x => x.JoinRequestId,
                        principalTable: "JoinRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonMaterials_AspNetUsers_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonMaterials_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformRevenueLedgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    CommissionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionRateApplied = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformRevenueLedgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformRevenueLedgers_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
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
                    { 12, null, null, "نوع المرفق", null },
                    { 20, null, null, "صلة القرابة", null }
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
                    { 1, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(7669), null, false, "مدير النظام", null, null },
                    { 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(8486), null, false, "الطالب", null, null },
                    { 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(8497), null, false, "المعلم", null, null },
                    { 4, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(8499), null, false, "ولي الامر", null, null }
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
                    { 18, null, null, "شهادة ماجستير", 12 },
                    { 21, null, null, "اب", 20 },
                    { 22, null, null, "ام", 20 },
                    { 23, null, null, "ابن", 20 },
                    { 24, null, null, "بنت", 20 },
                    { 25, null, null, "زوج", 20 },
                    { 26, null, null, "زوجة", 20 },
                    { 27, null, null, "وصي", 20 }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "DeletedBy", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, 1, null, new DateTime(2026, 10, 4, 14, 9, 44, 381, DateTimeKind.Local).AddTicks(5729), null, null, false, false, false, false, null, null, "الاب", "Parent Page", null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "DeletedBy", "Email", "EmailConfirmed", "GenderId", "IsActive", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedBy", "UpdatedOn", "UserName", "UserTypeId" },
                values: new object[] { "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3", 0, "default_avatar.png", "dddeb81c-6b1b-4ca4-a2f6-4e6a2be2cb42", null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(9266), null, "admin@Academia.com", false, 6, true, false, false, null, "Academia Admin", null, "ADMIN@Academia.COM", "0594727849Ziad#", "", false, "328a6a94-7fd5-4e72-b705-ee734053f85a", false, null, null, "admin@Academia.com", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "DeletedBy", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 2, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(6890), null, "bi bi-house-fill", true, true, false, false, "Home/Index", null, "الرئيسية", "Home", 1, null, null },
                    { 3, 1, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8885), null, "bi bi-list-ul", true, true, false, false, null, 1, "الإدارة", "Management", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "PageId", "UserTypeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "DeletedBy", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 4, 1, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8899), null, "bi bi-people", true, true, false, false, null, 1, "إدارة المستخدمين", "Users Management", 3, null, null },
                    { 8, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8926), null, "bi bi-geo-alt-fill", true, true, false, false, "Destination/Index", 1, "المحافظات و المدن", "Governorates and Cities", 3, null, null },
                    { 9, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8929), null, "bi bi-view-list", true, true, false, false, "Management/Modules", 1, "وحدات النظام", "Governorates and Cities", 3, null, null },
                    { 10, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8933), null, "bi bi-window-stack", true, true, false, false, "Page/Index", 1, "الصفحات", "Pages", 3, null, null },
                    { 11, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8936), null, "fa fa-anchor", true, true, false, false, "Constant/Index", 1, "الثوابت", "Constants", 3, null, null }
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
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "DeletedBy", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 5, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8902), null, "bi bi-person-fill", true, true, false, false, "User/Index", 1, "المستخدمين", "Users", 4, null, null },
                    { 6, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8911), null, "bi bi-people", true, true, false, false, "UserType/Index", 1, "أنواع المستخدمين", "User Types", 4, null, null },
                    { 7, 2, null, new DateTime(2026, 10, 4, 14, 9, 44, 383, DateTimeKind.Local).AddTicks(8914), null, "bi bi-check-lg", true, true, false, false, "UserPermission/Index", 1, "صلاحيات المستخدم", "User Permissions", 4, null, null },
                    { 26, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2092), null, null, false, true, true, false, "Destination/GetAll", 1, "عرض بيانات جدول المحافظات والمدن", "Display Governorates and Cities DateTable", 8, null, null },
                    { 27, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2095), null, null, false, true, true, false, "Destination/CreateEditModal", 1, "عرض واجهة إضافة تعديل وجهة", "Display create Edit Destination page", 8, null, null },
                    { 28, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2097), null, null, false, true, true, false, "Destination/CreateEdit", 1, "إضافة تعديل وجهة", "create Edit Destination", 8, null, null },
                    { 29, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2101), null, null, false, true, true, false, "Destination/Delete", 1, "حذف وجهة", "Delete Destination", 8, null, null },
                    { 30, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2103), null, null, false, true, true, false, "Management/SwitchStatus", 1, "تبديل حالات وحدات النظام", "Switching states of system Modules", 9, null, null },
                    { 31, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2106), null, null, false, true, true, false, "Page/GetAll", 1, "عرض بيانات جدول الصفحات", "Display Pages DataTable", 10, null, null },
                    { 32, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2108), null, null, false, true, true, false, "Page/CreateEditModal", 1, "عرض واجهة إضافة  تعديل صفحة", "Display Create Edit Page interface", 10, null, null },
                    { 33, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2111), null, null, false, true, true, false, "Page/CreateEdit", 1, "إضافة تعديل صفحة", "Create Edit Page", 10, null, null },
                    { 34, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2113), null, null, false, true, true, false, "Page/Delete", 1, "حذف صفحة", "Delete Page", 10, null, null },
                    { 35, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2116), null, null, false, true, true, false, "Constant/GetAll", 1, "عرض بيانات جدول الثوابت", "Display Constant DataTable", 11, null, null },
                    { 36, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2119), null, null, false, true, true, false, "Constant/CreateEditModal", 1, "عرض واجهة إضافة تعديل ثوابت", "Display Create Edit Constant Page", 11, null, null },
                    { 37, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2121), null, null, false, true, true, false, "Constant/CreateEdit", 1, "إضافة تعديل ثوابت", "Create Edit Constant", 11, null, null },
                    { 38, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2124), null, null, false, true, true, false, "Constant/Delete", 1, "حذف ثابت", "Delete Constant", 11, null, null }
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
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "DeletedBy", "Icon", "InMenu", "IsActive", "IsAjax", "IsDeleted", "Link", "ModuleId", "Name", "NameEn", "ParentId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 12, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(1533), null, null, false, true, true, false, "User/GetAll", 1, "عرض بيانات جدول المستخدمين", "Display User DataTable", 5, null, null },
                    { 13, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2034), null, null, false, true, true, false, "User/CreateEditModal", 1, "اظهار واجهة اضافة  تعديل مستخدم", "Display Create Edit User Page", 5, null, null },
                    { 14, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2044), null, null, false, true, true, false, "User/CreateEdit", 1, "اضافة تعديل مستخدم", "Create Edit User", 5, null, null },
                    { 15, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2049), null, null, false, true, true, false, "User/Delete", 1, "حذف مستخدم", "Delete User", 5, null, null },
                    { 16, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2052), null, null, false, true, true, false, "User/MyProfileModal", 1, "عرض واجهة ملفي الشخصي", "Display My Profile Page", 5, null, null },
                    { 17, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2058), null, null, false, true, true, false, "User/MyProfile", 1, "تعديل ملفي الشخصي", "Update My Profile", 5, null, null },
                    { 18, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2070), null, null, false, true, true, false, "User/ChangePasswordModal", 1, "عرض واجهة تغير كلمة المرور", "Display Change Password Page", 5, null, null },
                    { 19, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2072), null, null, false, true, true, false, "User/ChangePassword", 1, "تغير كلمة المرور", "ChangePassword", 5, null, null },
                    { 20, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2075), null, null, false, true, true, false, "UserType/GetAll", 1, "عرض بيانات جدول انواع المستخدين", "Display User Type DateTable", 6, null, null },
                    { 21, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2079), null, null, false, true, true, false, "UserType/CreateEditModal", 1, "عرض واجهة اضافة  تعديل نوع المستخدم", "Display Create Edit User Type page", 6, null, null },
                    { 22, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2081), null, null, false, true, true, false, "UserType/CreateEdit", 1, "اضافة تعديل نوع مستخدم", "Create Edit User Type ", 6, null, null },
                    { 23, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2084), null, null, false, true, true, false, "UserType/Delete", 1, "حذف نوع مستخدم", "Delete User Type ", 6, null, null },
                    { 24, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2087), null, null, false, true, true, false, "UserPermission/GetUserTypePermissions", 1, "عرض صلاحيات نوع المستخدم", "display User Type Permissions", 7, null, null },
                    { 25, 3, null, new DateTime(2026, 10, 4, 14, 9, 44, 385, DateTimeKind.Local).AddTicks(2089), null, null, false, true, true, false, "UserPermission/SavePermissions", 1, "حفظ صلاحيات نوع المستخدم", "Save User Type Permissions", 7, null, null }
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
                name: "IX_Attendances_GroupId_SessionDate",
                table: "Attendances",
                columns: new[] { "GroupId", "SessionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_RecordedBy",
                table: "Attendances",
                column: "RecordedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRescheduleRequests_BookingId",
                table: "BookingRescheduleRequests",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GradeId",
                table: "Bookings",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StudentId",
                table: "Bookings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SubjectId",
                table: "Bookings",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TeacherId_Date_StartTime",
                table: "Bookings",
                columns: new[] { "TeacherId", "Date", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Constants_ParentId",
                table: "Constants",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GroupId",
                table: "Enrollments",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_JoinRequestId",
                table: "Enrollments",
                column: "JoinRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_GradedBy",
                table: "ExamResults",
                column: "GradedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_StudentId",
                table: "ExamResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseId",
                table: "Exams",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_GroupId",
                table: "Exams",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GradeId",
                table: "Groups",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupScheduleDays_GroupId_DayOfWeek",
                table: "GroupScheduleDays",
                columns: new[] { "GroupId", "DayOfWeek" });

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_CourseId",
                table: "JoinRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_DecisionBy",
                table: "JoinRequests",
                column: "DecisionBy");

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_GroupId",
                table: "JoinRequests",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_StudentId",
                table: "JoinRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMaterials_LessonId",
                table: "LessonMaterials",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMaterials_UploadedBy",
                table: "LessonMaterials",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId_ScheduledDate",
                table: "Lessons",
                columns: new[] { "CourseId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId_ScheduledDate",
                table: "Lessons",
                columns: new[] { "GroupId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PlatformRevenueLedgers_EnrollmentId",
                table: "PlatformRevenueLedgers",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FatherId",
                table: "Students",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                table: "Students",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAvailabilities_TeacherId_DayOfWeek",
                table: "TeacherAvailabilities",
                columns: new[] { "TeacherId", "DayOfWeek" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeLevels_GradeId",
                table: "TeacherGradeLevels",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGradeLevels_TeacherId_GradeId",
                table: "TeacherGradeLevels",
                columns: new[] { "TeacherId", "GradeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRatings_BookingId",
                table: "TeacherRatings",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRatings_StudentId",
                table: "TeacherRatings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRatings_TeacherId",
                table: "TeacherRatings",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GradeId",
                table: "Teachers",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherId_SubjectId",
                table: "TeacherSubjects",
                columns: new[] { "TeacherId", "SubjectId" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTopUpRequests_StudentId",
                table: "WalletTopUpRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTopUpRequests_VerifiedBy",
                table: "WalletTopUpRequests",
                column: "VerifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_DecisionBy",
                table: "WalletTransactions",
                column: "DecisionBy");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletId_CreatedOn",
                table: "WalletTransactions",
                columns: new[] { "WalletId", "CreatedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_ApprovedBy",
                table: "WithdrawalRequests",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_InstructorId",
                table: "WithdrawalRequests",
                column: "InstructorId");
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
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BookingRescheduleRequests");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "GroupScheduleDays");

            migrationBuilder.DropTable(
                name: "LessonMaterials");

            migrationBuilder.DropTable(
                name: "Migrations");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ParentStudentLinks");

            migrationBuilder.DropTable(
                name: "PlatformCommissionSettings");

            migrationBuilder.DropTable(
                name: "PlatformRevenueLedgers");

            migrationBuilder.DropTable(
                name: "TeacherAvailabilities");

            migrationBuilder.DropTable(
                name: "TeacherGradeLevels");

            migrationBuilder.DropTable(
                name: "TeacherRatings");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "TrackStudentTransfers");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "WalletTopUpRequests");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "WithdrawalRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "JoinRequests");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "PageCategories");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Fathers");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
