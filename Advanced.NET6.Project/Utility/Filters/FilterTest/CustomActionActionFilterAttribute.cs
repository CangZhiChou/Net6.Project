using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced.NET6.Project.Utility.Filters.FilterTest
{
    public class CustomActionActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomControllerActionFilterAttribute.OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomControllerActionFilterAttribute.OnActionExecuted");
        } 
    }
}
