using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReferenceProject.Middleware
{
    public class PreventResponseCachingMiddleware
    {
        RequestDelegate Next { get; }

        public PreventResponseCachingMiddleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if( context.Request.Method == "GET")
            {
                context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
            }

            await Next(context);
        }
    }
}
