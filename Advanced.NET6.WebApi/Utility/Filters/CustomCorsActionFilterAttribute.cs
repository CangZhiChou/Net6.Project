using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced.NET6.WebApi.Utility.Filters
{
    public class CustomCorsActionFilterAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

       
    }
}
