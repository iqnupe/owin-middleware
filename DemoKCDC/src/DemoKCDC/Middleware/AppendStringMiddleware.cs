using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace DemoKCDC.Middleware
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
            await context.Response.WriteAsync($"Hey! Your lucky number is: {_numberMaker.MakeNumber()}");
        }
    }
}