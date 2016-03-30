using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace MiddlewareCpl.CplCode
{
    public class AppendStringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberGenerator numberGenerator;

        public AppendStringMiddleware(RequestDelegate next, INumberGenerator numberGenerator)
        {
            _next = next;
            this.numberGenerator = numberGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            await context.Response.WriteAsync($"Hello From Middleware! Your magic number is {numberGenerator.GenerateNumber()}");
        }
    }
}