using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication1.Context;
using WebApplication1.Filters;
using WebApplication1.Mapping;
using WebApplication1.Middlewares;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });
            builder.Services.AddAuthentication()
     .AddGoogle(options =>
     {
         options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
         options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
     });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            builder.Services.AddControllersWithViews(

                options=> { options.Filters.Add<ExceptionHandlerFilter>(); options.Filters.Add<AddInfoToResponse>(); }
             
                );
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<ICourseRepo, CourseRepo>();
            // 3. Configure Session options
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // How long the session lasts
                options.Cookie.HttpOnly = true;                // Security: prevents JS access
                options.Cookie.IsEssential = true;  
            });
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCustomLoggingMiddleware();
            app.UseSession();
            //app.UseTestMiddleware();
            app.UseGlobalErrorHandler();
            //app.UseMiddleware<MyMiddleware>();

            app.UseRouting();
            app.UseAuthentication();   
            app.UseAuthorization();

            //app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            MapsterConfig.RegisterMappings();
            app.Run();
        }
    }
}
