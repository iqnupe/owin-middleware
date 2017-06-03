using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicCityMiddleware.Middleware
{
    public class OutputStreamMiddleware
    {
        private readonly RequestDelegate _next;

        public OutputStreamMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalOutputStream = context.Response.Body;
            var replacementOutputStream = new MemoryStream();
            context.Response.Body = replacementOutputStream;

            await _next(context);

            context.Response.Body = originalOutputStream;
            replacementOutputStream.Position = 0;
            await context.Response.Body.WriteAsync(replacementOutputStream.GetBuffer(), 0,
                (int) replacementOutputStream.Length);

        }
    }
}