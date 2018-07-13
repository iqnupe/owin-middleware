using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KcMiddleware.Middleware
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
            // Before
            await _next(context);
            // After
        }
    }}