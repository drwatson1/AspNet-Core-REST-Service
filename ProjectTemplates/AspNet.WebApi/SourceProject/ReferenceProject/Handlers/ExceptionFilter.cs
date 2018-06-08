using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace ReferenceProject
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private class ErrorResponse
        {
            public ErrorResponse(Exception ex)
            {
                Error = new ErrorDescription()
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException != null ? ex.InnerException.Message : null
                };
            }

            public class ErrorDescription
            {
                public string Message { get; set; }
                public string InnerException { get; set; }
            }

            public ErrorDescription Error { get; set; }
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var responseContent = new ErrorResponse(actionExecutedContext.Exception);

            var content = new ObjectContent<ErrorResponse>(responseContent, new JsonMediaTypeFormatter());

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ObjectContent<ErrorResponse>(responseContent, new JsonMediaTypeFormatter())
            };
        }
    }
}