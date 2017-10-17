using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DevUpMiddleware.Devup
{
    public class DistanceMiddleware
    {
        private readonly RequestDelegate _next;

        public DistanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var pathString = context.Request.Path.Value;
            var match = Regex.Match(pathString,
                @"/distance/(?<Lat1>[\d.-]+),(?<Lon1>[\d.-]+)/(?<Lat2>[\d.-]+),(?<Lon2>[\d.-]+)");

            if (match.Success)
            {
                var inputLat1 = double.Parse(match.Groups["Lat1"].Value);
                var inputLon1 = double.Parse(match.Groups["Lon1"].Value);
                var inputLat2 = double.Parse(match.Groups["Lat2"].Value);
                var inputLon2 = double.Parse(match.Groups["Lon2"].Value);

                var result = DistanceTo(inputLat1, inputLon1, inputLat2, inputLon2);

                await context.Response.WriteAsync($"The distance between these 2 points is {result:0.0} miles.");
            }
            else
            {
                await _next(context);
            }
        }

        // Taken from https://stackoverflow.com/a/24712129
        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist;
        }
    }
}