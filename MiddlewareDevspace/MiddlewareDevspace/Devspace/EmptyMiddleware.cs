using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDevspace.Devspace
{
    public class EmptyMiddleware
    {
        private readonly RequestDelegate _next;

        public EmptyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }}