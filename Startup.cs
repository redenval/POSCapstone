using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Capstone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Services;
using Capstone.Services.IServices;
using Capstone.Repository;
using Capstone.Repository.IRepository;

namespace Capstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Services
            services.AddScoped<ISessionService, SessionService>();

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Store",
                    pattern: "/Store",
                    defaults: new { controller = "Home", action = "Store" });
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "/Login",
                    defaults: new { controller = "Home", action = "Login" });
                endpoints.MapControllerRoute(
                    name: "Register",
                    pattern: "/Register",
                    defaults: new { controller = "Home", action = "Register" });
                endpoints.MapControllerRoute(
                    name: "Cart",
                    pattern: "/Cart",
                    defaults: new { controller = "Home", action = "Cart" });
                endpoints.MapControllerRoute(
                    name: "ViewOrders",
                    pattern: "/ViewOrders",
                    defaults: new { controller = "Home", action = "ViewOrders" });
                endpoints.MapControllerRoute(
                    name: "OrderProcess",
                    pattern: "/OrderBeingProcess",
                    defaults: new { controller = "Home", action = "OrderBeingProcess" });
                endpoints.MapControllerRoute(
                    name: "Profile",
                    pattern: "/Profile",
                    defaults: new { controller = "Home", action = "Profile" });
                endpoints.MapControllerRoute(
                    name: "About",
                    pattern: "/About",
                    defaults: new { controller = "Home", action = "About" });
            });
        }
    }
}
