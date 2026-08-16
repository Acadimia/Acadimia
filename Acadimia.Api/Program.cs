using Acadimia.Data.DbContext;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Models;
//using Acadimia.Infrastructure.AutoMapper;
using Acadimia.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acadimia", Version = "v1" });
    //c.AddSecurityDefinition("Bearer",
    //    new OpenApiSecurityScheme
    //    {
    //        Description = "Please enter into field the word 'Bearer' following by space and JWT",
    //        Name = "Authorization",
    //        In = ParameterLocation.Header,
    //        Scheme = "Bearer"
    //    });
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            },
    //            Scheme = "oauth2",
    //            Name = "Bearer",
    //            In = ParameterLocation.Header,
    //        },
    //        new List<string>()
    //    }
    //});
});
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
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acadimia");
});

app.MapStaticAssets();

app.Run();