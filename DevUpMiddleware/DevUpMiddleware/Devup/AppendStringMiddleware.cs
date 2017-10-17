using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DevUpMiddleware.Devup
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
            await context.Response.WriteAsync($"I hope your number is over 9000. Here it is: {_numberMaker.MakeNumber()}");
            await _next(context);
        }
    }}