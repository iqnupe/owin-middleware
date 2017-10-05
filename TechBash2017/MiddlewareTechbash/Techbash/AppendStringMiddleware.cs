using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareTechbash.Techbash
{
    public class AppendStringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberMaker _numberMaker;

        public AppendStringMiddleware(RequestDelegate next, INumberMaker numberMaker)
        {
            _next = next;
            _numberMaker = numberMaker;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            await context.Response.WriteAsync($"Hello Numbers! {_numberMaker.MakeNumber()}");
        }
    }
}