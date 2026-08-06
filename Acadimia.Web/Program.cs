using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
//using Acadimia.Infrastructure.AutoMapper;
using Acadimia.Infrastructure.Extentions;
//using Acadimia.Web.Helper.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    ;


builder.Services.AddControllersWithViews();

// Moved up: must be configured before Build()
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Auth/login");
    options.LogoutPath = new PathString("/Auth/logout");
    //options.AccessDeniedPath = new PathString("/Auth/Accessdenied");
});
//builder.Services.AddTransient<IClaimsService, ClaimsService>();
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.RegisterServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();