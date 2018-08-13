using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ReferenceProject
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // TODO: Add another exceptions
            if (actionExecutedContext.Exception is KeyNotFoundException)
            {
                // Replace 500 Internal Server Error to 404 Not Found for KeyNotFoundException
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, actionExecutedContext.Exception.Message);
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception);
            }
        }
    }
}