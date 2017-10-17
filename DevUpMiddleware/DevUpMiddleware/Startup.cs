using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevUpMiddleware.Devup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevUpMiddleware
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
            services.AddNumberMaking();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Use(next =>
            //{
            //    RequestDelegate rd = async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //        await next(context);
            //    };

            //    return rd;
            //});

            app.UseNumberMaking();
            //app.UseMiddleware<ZipCodeLookupMiddleware>();
            //app.UseMiddleware<DistanceMiddleware>();
            //app.UseMiddleware<InjectingMiddleware>();

            app.UseAuthentication();

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