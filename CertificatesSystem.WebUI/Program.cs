using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Services;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using CertificatesSystem.Models.Data.Identity;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddControllersWithViews().AddRazorRuntimeCompilation();
services.AddRazorPages();

// Dependency Injection - Services
services.AddAutoMapper(typeof(Program));
services.AddTransient<IStudentsService, StudentsService>();
services.AddTransient<IManagersService, ManagersService>();
services.AddTransient<IEnrollmentService, EnrollmentService>();
services.AddTransient<IMiscellanyService, MiscellanyService>();
services.AddTransient<IUsersService, UsersService>();
services.AddTransient<IEmailService, EmailService>();

var connectionString = configuration.GetConnectionString("CertificatesSystemPostgresqlHeroku");

// Database context
// services.AddDbContext<CertificatesSystemContext>(options => options.UseSqlServer(connectionString));
services.AddDbContext<CertificatesSystemContext>(options => options.UseNpgsql(connectionString));

// Identity context
services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// Identity implementation and configuration
services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
.AddRoles<ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Identity.Cookie";
    config.LoginPath = "/Identity/Account/Login";
    config.AccessDeniedPath = "/Identity/Account/AccessDenied";
    config.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "enrollment",
        pattern: "Enrollment/{year}/{grade}",
        defaults: new { controller = "Enrollment", action = "Index", year = DateTime.Now.Year, grade = 1 });

    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Map Identity Razor Pages
    endpoint.MapRazorPages();
});

// Rotativa configuration
RotativaConfiguration.Setup(env.WebRootPath,"../Rotativa");

app.Run();