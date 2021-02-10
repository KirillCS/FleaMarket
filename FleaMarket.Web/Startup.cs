using AutoMapper;
using FleaMarket.Business.Services;
using FleaMarket.Domain;
using FleaMarket.Domain.Repositories;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FleaMarket.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = string.Empty;
            })
                    .AddEntityFrameworkStores<DatabaseContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<ApplicationConfigurations>(this.Configuration.GetSection("ApplicationConfigurations"));

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IFormFileSaver, FormFileSaver>();

            services.AddTransient<IItemService, ItemService>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var supportedCulters = new[] { "en" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCulters[0])
                                                                      .AddSupportedCultures(supportedCulters)
                                                                      .AddSupportedUICultures(supportedCulters);

            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
