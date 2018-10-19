using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using System.Net.Mime;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReferenceProject.Middleware
{
    public class ExceptionMiddleware
    {
        RequestDelegate Next { get; }
        public ILogger Logger { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment env)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = 500;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            if( ex is KeyNotFoundException )
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }

            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorResponse(ex), new JsonSerializerSettings() {  ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            if(context.Response.StatusCode == 500)
            {
                Logger.LogError(ex, "Unhandled exception occurred");
            }
        }

        class ErrorResponse
        {
            public ErrorResponse(Exception ex)
            {
                Error = new ExceptionDescription(ex);
            }

            public ExceptionDescription Error { get; set; }
        }

        class ExceptionDescription
        {
            public ExceptionDescription(Exception ex)
            {
                Message = ex.Message;
            }

            public string Message { get; set; }
        }

    }
}
