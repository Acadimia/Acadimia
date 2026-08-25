using Acadimia.Api.Helper.Claims;
using Acadimia.Api.Helper.Files;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.AutoMapper;
using Acadimia.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add DbContext with Transient Error Resiliency
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // تفعيل إعادة المحاولة التلقائية لتجنب أخطاء انقطاع الاتصال العابرة
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    })
    .ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
                "https://your-frontend-domain.com",   // production frontend
                "http://localhost:3000"                // local dev (React/Vue/Angular default)
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // needed if you use cookies (your Identity cookie auth relies on this)
    });
});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Auth/login");
    options.LogoutPath = new PathString("/Auth/logout");
    // options.AccessDeniedPath = new PathString("/Auth/Accessdenied");
});

// AutoMapper 
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
// File Service.
builder.Services.AddTransient<IFileService, FileService>();
// Claims Service.
builder.Services.AddScoped<IClaimsService, ClaimsService>();
// Register Custom Services
builder.Services.RegisterServices();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acadimia", Version = "v1" });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("FrontendPolicy");

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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acadimia");
});

app.MapStaticAssets();
app.MapControllers();  

app.Run();