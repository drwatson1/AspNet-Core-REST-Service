using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReferenceProject.Filters
{
    /// <summary>
    /// Filter to check is a model valid
    /// </summary>
    /// <remarks>See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#model-validation</remarks>
    public class ValidateModelFilter: IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
