using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        private IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<StoreDbContext>(opts =>
            {
                opts.UseSqlServer(
  Configuration["ConnectionStrings:SportsStoreConnection"]);

            });
            services.AddScoped<IStoreRepository, EFStoreRepository>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute("catpage",
                
                "{category}/Page{productPage:int}",
                new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page",
                "Page{productPage:int}",
                new
                {
                    Controller = "Home",
                    action = "Index",
                    productPage = 1
                });
                endpoints.MapControllerRoute("category", "{category}",
                new
                { productPage = 1,
                    Controller = "Home",
                    action = "Index",

                });
                endpoints.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new
    {
        Controller = "Home",
        action = "Index",
        productPage = 1
    });
                endpoints.MapDefaultControllerRoute();
            });
            SeedData.EnsurePopulated(app);
        }
    }
}




