using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CodemashMiddleware.Codemash
{
    public static class ExtensionMethods
    {
        public static void UseInjection(this IApplicationBuilder app)
        {
            app.UseMiddleware<InjectingMiddleware>();
        }

        public static void AddNumberMaker(this IServiceCollection services)
        {
            services.AddSingleton<INumberMaker, AllTwelvesNumberMaker>();
        }
    }
}