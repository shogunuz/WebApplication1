using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.AddCors();
            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );

            services.AddDbContext<CountryContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Calc","{controller}/{action}/{x}/{y}");

                endpoints.MapControllerRoute(
                    name: "Proba", 
                    pattern: "{controller=Calc}/{action=Index}"); // route template состоит из 2х сегментов
                                                                   // endpoints.MapControllerRoute("Proba", "{controller}/{action}");

               // endpoints.MapControllerRoute(name:"Regexnk", 
                 //   pattern: "testlinks/{controller}/{somesign:regex([a-z]{{3}}\\d{{3}})}/{action}");

                endpoints.MapControllerRoute(
                    name: "Regexcalc",
                    pattern: "newcalcs/{controller}/{action}/{somevalue}",
                    defaults: new
                    {
                        controller = "test",
                        action = "newcalc"
                    },
                    constraints: new
                    {
                        somevalue = new RegexRouteConstraint("[a-z]{2}\\d{2}")
                    });
                endpoints.MapControllerRoute(
                    name: "Generatorssilok",
                    pattern: "newlinks/{controller}/{action}/{id?}"
                    );
            });

        }
    }
}
