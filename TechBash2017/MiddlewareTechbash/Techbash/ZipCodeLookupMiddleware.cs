using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiddlewareTechbash.Techbash
{
    public class ZipCodeLookupMiddleware
    {
        private readonly RequestDelegate _next;

        private Dictionary<string, LatLong> _zipcodes;

        public ZipCodeLookupMiddleware(RequestDelegate next)
        {
            _next = next;
            _zipcodes = new Dictionary<string, LatLong>
            {
                {"48084", new LatLong() {Lat = 42.5597, Long = -83.1762}},
                {"18349", new LatLong() {Lat = 41.1033, Long = -75.3675}}
            };
        }

        public async Task Invoke(HttpContext context)
        {
            var pathString = context.Request.Path.Value;
            var match = Regex.Match(pathString, @"/zipcodes/(?<zipcode>\d+)");

            if (match.Success)
            {
                var input = match.Groups["zipcode"].Value;
                LatLong result;
                if (_zipcodes.TryGetValue(input, out result))
                {
                    await context.Response.WriteAsync($"{result.Lat},{result.Long}");
                }
                else
                {
                    await context.Response.WriteAsync($"Zipcode not in database.");
                }
            }
            else
            {
                await _next(context);
            }
        }

    }

    public class LatLong
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}