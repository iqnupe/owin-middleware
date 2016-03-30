using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace MiddlewareCpl.CplCode
{
    public class InjectingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberGenerator generator;

        public InjectingMiddleware(RequestDelegate next, INumberGenerator generator)
        {
            _next = next;
            this.generator = generator;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalOutputStream = context.Response.Body;
            var replacementOutputStream = new MemoryStream();
            context.Response.Body = replacementOutputStream;

            await _next(context);

            context.Response.Body = originalOutputStream;

            replacementOutputStream.Position = 0;
            using (var sr = new StreamReader(replacementOutputStream))
            {
                var streamDump = await sr.ReadToEndAsync();
                if (context.Items["injected"] != null)
                {
                    var injectedGuid = (string)context.Items["injected"];
                    streamDump = streamDump.Replace(injectedGuid, $"The number {generator.GenerateNumber()} has been injected from the middleware!");
                }
                var replacedBytes = Encoding.ASCII.GetBytes(streamDump);
                await context.Response.Body.WriteAsync(replacedBytes, 0, replacedBytes.Length);
            }
        }
    }
}