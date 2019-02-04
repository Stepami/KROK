using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using smartdressroom.Binders;
using smartdressroom.Services;

namespace smartdressroom
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
            /*
             * Если это включено, сессия не работает
             * 
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.;
            });
            */

            // Поддержка сессий - до MVC!
            // См. также например
            // https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/app-state?view=aspnetcore-2.0
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".smartdressroom";
            });

            services.AddTransient<IStorageService, StorageService>();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new CartModelBinderProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            // Поддержка сессий - до MVC!
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "login",
                    "ClothesModels",
                    new { controller = "ClothesModels", action = "Login" });
                routes.MapRoute(
                    "afterlogin",
                    "ClothesModels/Index",
                    new { controller = "ClothesModels", action = "Index" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
