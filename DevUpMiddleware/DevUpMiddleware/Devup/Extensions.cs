using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DevUpMiddleware.Devup
{
    public static class Extensions
    {
        public static void AddNumberMaking(this IServiceCollection services)
        {
            services.AddSingleton<INumberMaker, Over9000NumberMaker>();
        }

        public static void UseNumberMaking(this IApplicationBuilder app)
        {
            app.UseMiddleware<OutputStreamMiddleware>();
            app.UseMiddleware<AppendStringMiddleware>();
        }
    }
}