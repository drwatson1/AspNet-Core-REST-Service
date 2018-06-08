using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

namespace ReferenceProject
{
    public static class CacheControlMiddleware
    {
        public static Task Middleware(IOwinContext context, Func<Task> next)
        {
            if (context.Request.Method == "GET")
            {
                context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
            }

            return next();
        }

        public static void PreventResponseCaching(this IAppBuilder app)
        {
            app.Use(Middleware);
        }
    }
}