using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDevspace.Devspace
{
    public class InjectingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberMaker _numberMaker;

        public InjectingMiddleware(RequestDelegate next, INumberMaker numberMaker)
        {
            _next = next;
            _numberMaker = numberMaker;
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
                    var injectedGuid = (string) context.Items["injected"];
                    streamDump = streamDump.Replace(injectedGuid, $"This has been injected from the middleware! Your lucky number is {_numberMaker.MakeNumber()}");
                }
                var replacedBytes = Encoding.ASCII.GetBytes(streamDump);
                await context.Response.Body.WriteAsync(replacedBytes, 0, replacedBytes.Length);
            }
        }
    }
}