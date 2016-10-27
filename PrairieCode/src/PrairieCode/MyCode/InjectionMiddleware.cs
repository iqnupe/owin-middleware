using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrairieCode.MyCode
{
    public class InjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INumberGenerator _numberGenerator;

        public InjectionMiddleware(RequestDelegate next, INumberGenerator numberGenerator)
        {
            _next = next;
            _numberGenerator = numberGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalOutputStream = context.Response.Body;
            var replacementOutputStream = new MemoryStream();
            context.Response.Body = replacementOutputStream;

            await _next(context);

            replacementOutputStream.Position = 0;
            using (var sr = new StreamReader(replacementOutputStream))
            {
                var streamDump = await sr.ReadToEndAsync();
                if (context.Items["injected"] != null)
                {
                    var injectedGuid = (string)context.Items["injected"];
                    streamDump = streamDump.Replace(injectedGuid, $"The lucky number is {_numberGenerator.GenerateNumber()}");
                }
                var replacedBytes = Encoding.ASCII.GetBytes(streamDump);
                await originalOutputStream.WriteAsync(replacedBytes, 0, replacedBytes.Length);
            }
        }
    }
}