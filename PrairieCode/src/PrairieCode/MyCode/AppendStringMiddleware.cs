using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrairieCode.MyCode
{
    public class AppendStringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberGenerator _numberGenerator;

        public AppendStringMiddleware(RequestDelegate next, INumberGenerator numberGenerator)
        {
            _next = next;
            _numberGenerator = numberGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            await context.Response.WriteAsync($"Your lucky number is: {_numberGenerator.GenerateNumber()}!!!!");
        }
    }
}