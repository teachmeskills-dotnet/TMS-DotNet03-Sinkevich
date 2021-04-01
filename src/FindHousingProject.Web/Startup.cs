using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Managers;
using FindHousingProject.BLL.Repositories;
using FindHousingProject.DAL;
using FindHousingProject.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FindHousingProject.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Housing>, Repository<Housing>>();
            services.AddScoped<IRepository<Place>, Repository<Place>>();
            services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();
            // Managers
            services.AddScoped<IUserManager, UsManager>();
            services.AddScoped<IHousingManager, HousingManager>();
            services.AddScoped<IReservationManager, ReservationManager>();
            // Microsoft services
            services.AddControllersWithViews();
            // Database context
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            // ASP.NET Core Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "TeachMeSkills.Cookie";
                config.LoginPath = "/Account/SignIn";
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
