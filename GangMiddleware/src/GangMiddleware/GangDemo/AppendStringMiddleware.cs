using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace GangMiddleware.GangDemo
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
            await context.Response.WriteAsync($"Hello! Your special magic number is {numberGenerator.GenerateNumber()}!");
            //await _next(context);
        }
    }
}