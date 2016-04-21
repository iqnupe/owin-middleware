using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GangMiddleware.GangDemo;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GangMiddleware.Models;
using GangMiddleware.Services;
using Microsoft.AspNet.Http;

namespace GangMiddleware
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();


            var env2 = Configuration["MyEnvironment"];
            ;
            builder
                .AddJsonFile($"appsettings.{env2}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddTransient<INumberGenerator, AllSevensNumberGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseMiddleware<AppendStringMiddleware>();
            //app.UseMiddleware<InjectingMiddleware>();

            app.Use(next =>
            {
                RequestDelegate rd = async context =>
                {
                    //await context.Response.WriteAsync($"The Configuration setting SomeKey has a value of {Configuration["SomeKey"]}");
                    //await context.Response.WriteAsync($" and the other setting is {Configuration["SomeKey2"]}");
                    await next(context);
                };

                return rd;
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");

            //    // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            //    try
            //    {
            //        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            //            .CreateScope())
            //        {
            //            serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
            //                 .Database.Migrate();
            //        }
            //    }
            //    catch { }
            //}

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
