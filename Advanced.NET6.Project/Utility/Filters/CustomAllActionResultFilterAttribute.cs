using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced.NET6.Project.Utility.Filters
{
    public class CustomAllActionResultFilterAttribute : ActionFilterAttribute
    {

        private readonly ILogger<CustomAllActionResultFilterAttribute> _ILogger;
        public CustomAllActionResultFilterAttribute(ILogger<CustomAllActionResultFilterAttribute> iLogger)
        {
            this._ILogger = iLogger;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var para = context.HttpContext.Request.QueryString.Value;
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");
            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法；参数为：{para}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(context.Result);
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");
            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法:执行结果为：{result}");
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");

            var para = context.HttpContext.Request.QueryString.Value;
            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法；参数为：{para}");

            ActionExecutedContext executedContext = await next.Invoke(); //这句话执行就是去执行Action  

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(executedContext.Result);
            _ILogger.LogInformation($"执行{controllerName}控制器--{actionName}方法:执行结果为：{result}");
        }


        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
    }
}
