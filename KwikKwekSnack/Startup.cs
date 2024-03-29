using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;


namespace KwikKwekSnack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KwikKwekSnackContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("KwikKwekSnackSolution")));
            services.AddScoped<IDrinkRepo, DrinkRepoSql>();
            services.AddScoped<ISnackRepo, SnackRepoSql>();
            services.AddScoped<IExtraRepo, ExtraRepoSql>();
            services.AddScoped<IOrderRepo, OrderRepoSql>();
            services.AddScoped<IDrinkSizeRepo, DrinkSizeRepoSql>();            
            services.AddControllersWithViews();            
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
