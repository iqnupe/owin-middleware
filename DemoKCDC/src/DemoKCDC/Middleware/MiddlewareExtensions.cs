//using Microsoft.AspNet.Builder;
//using Microsoft.AspNet.Http;

//namespace DemoKCDC.Middleware
//{
//    public static class MiddlewareExtensions
//    {
//        public static void CallMyMiddleware(this IApplicationBuilder app)
//        {
//            app.Use(AppendStringMiddleware);
//            app.Use(AppendStringMiddleware);
//            app.Use(AppendStringMiddleware);
//        }

//        static RequestDelegate AppendStringMiddleware(RequestDelegate next)
//        {
//            RequestDelegate rd = async context =>
//            {
//                await next(context);
//                await context.Response.WriteAsync("Hello KCDC!");
//            };

//            return rd;
//        }
//    }
//}