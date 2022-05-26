using CertificatesSystem.Models.Data.Database;
using CertificatesSystem.Models.Interfaces;
using CertificatesSystem.Services;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Dependency Injection - Services
services.AddAutoMapper(typeof(Program));
services.AddTransient<IStudentsService, StudentsService>();
services.AddTransient<IManagersService, ManagersService>();
services.AddTransient<IEnrollmentService, EnrollmentService>();
services.AddTransient<IMiscellanyService, MiscellanyService>();

// Database context
var connectionString = configuration.GetConnectionString("CertificatesSystem");

services.AddDbContext<CertificatesSystemContext>(options => options.UseSqlServer(connectionString));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "enrollment",
    pattern: "Enrollment/{year}/{grade}",
    defaults: new { controller = "Enrollment", action = "Index", year = DateTime.Now.Year, grade = 1 });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rotativa configuration
RotativaConfiguration.Setup(env.WebRootPath,"../Rotativa");

app.Run();