using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodemashMiddleware.Codemash;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodemashMiddleware
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
            services.AddNumberMaker();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMiddleware<OutputStreamMiddleware>();
            //app.UseMiddleware<AppendStringMiddleware>();
            //app.UseMiddleware<ZipCodeLookupMiddleware>();
            //app.UseMiddleware<DistanceMiddleware>();

            app.UseInjection();

            //app.Use(next =>
            //{
            //    RequestDelegate rd = async context =>
            //    {
            //        await next(context);
            //        await context.Response.WriteAsync("Hello World!");
            //    };

            //    return rd;
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}