using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SeValleyMiddleware.Middleware
{
    public class ZipCodeLookupMiddleware
    {
        private readonly RequestDelegate _next;

        private Dictionary<string, LatLong> _zipcodes;

        public ZipCodeLookupMiddleware(RequestDelegate next)
        {
            _next = next;

            _zipcodes = TryLoadFromFile();

            if (_zipcodes == null)
                _zipcodes = new Dictionary<string, LatLong>
                {
                    {"48084", new LatLong() {Lat = 42.5597, Long = -83.1762}}, // Troy, MI
                    {"44870", new LatLong() {Lat = 41.45, Long = -82.71}}, // Sandusky, OH
                    {"37203", new LatLong() {Lat = 36.17, Long = -86.78}}, // Nashville, TN
                    {"18349", new LatLong() {Lat = 41.1033, Long = -75.3675}}, // Poconos Mountains, PA
                    {"85225", new LatLong() {Lat = 33.3092, Long = -111.8211}}, // Chandler, AZ
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

        /// <summary>
        /// This method exists purely to set up the live demo and is not intended to be used as an example of best practices.
        /// </summary>
        /// <returns>null</returns>
        private Dictionary<string, LatLong> TryLoadFromFile()
        {
            try
            {
                var text = File.ReadAllLines(@"C:\dev\talks\middleware\zipcodes.csv")
                    .Select(line =>
                    {
                        var cols = line.Split(',');
                        var zip = cols[0].PadLeft(5, '0');
                        double lat;
                        double lng;

                        double.TryParse(cols[1], out lat);
                        double.TryParse(cols[2], out lng);

                        return new {zip, latlong = new LatLong() {Lat = lat, Long = lng}};
                    })
                    .ToDictionary(o => o.zip, o => o.latlong);

                return text;
            }
            catch
            {
                return null;
            }
        }
    }

    public class LatLong
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }}