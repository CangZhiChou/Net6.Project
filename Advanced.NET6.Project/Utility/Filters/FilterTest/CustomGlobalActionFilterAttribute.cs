using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced.NET6.Project.Utility.Filters.FilterTest
{
    public class CustomGlobalActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomGlobalActionFilterAttribute.OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomGlobalActionFilterAttribute.OnActionExecuted");
        } 
    }
}
