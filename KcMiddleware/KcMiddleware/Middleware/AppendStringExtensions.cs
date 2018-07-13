using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KcMiddleware.Middleware
{
    public static class AppendStringExtensions
    {
        public static void UseAppendString(this IApplicationBuilder app)
        {
            app.UseMiddleware<AppendStringMiddleware>();
        }

        public static void AddAppendString(this IServiceCollection services)
        {
            services.AddTransient<INumberMaker, AllSevensNumberMaker>();
        }
    }
}