using Microsoft.AspNetCore.Mvc.Filters;

namespace ReferenceProject.Filters
{
    public class CacheControlFilter: IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if( context.HttpContext.Request.Method == "GET" )
            {
                context.HttpContext.Response.Headers.Add(
                    "Cache-Control", new string[] { "no-cache, no-store, must-revalidate" });
            }
        }
    }
}
