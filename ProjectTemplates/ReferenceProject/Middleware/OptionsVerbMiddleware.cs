using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ReferenceProject.Middleware
{
    // https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
    // https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS#Preflighted_requests
    // https://developer.mozilla.org/ru/docs/Web/HTTP/Methods/OPTIONS
    public class OptionsVerbMiddleware
    {
        RequestDelegate Next { get; }

        public OptionsVerbMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                return Task.CompletedTask;
            }
            return Next.Invoke(context);
        }
    }
}
