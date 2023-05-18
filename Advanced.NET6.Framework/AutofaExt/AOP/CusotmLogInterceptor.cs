using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Framework.AutofaExt.AOP
{
    public class CusotmLogInterceptor : IInterceptor
    {
        private readonly ILogger<CusotmLogInterceptor> _ILogger;
        public CusotmLogInterceptor(ILogger<CusotmLogInterceptor> logger)
        {
            this._ILogger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            {
                Console.WriteLine("Before");
                _ILogger.LogInformation($"参数：{Newtonsoft.Json.JsonConvert.SerializeObject(invocation.Arguments)}");
            }
            _ILogger.LogInformation($"方法{invocation.Method.Name} 开始执行");
            invocation.Proceed(); //这句话的执行就是要去执行真实的方法
            _ILogger.LogInformation($"方法{invocation.Method.Name} 执行完毕");
            {
                Console.WriteLine("After");
                _ILogger.LogInformation($"计算结果：{Newtonsoft.Json.JsonConvert.SerializeObject(invocation.ReturnValue)}");
            }

        }
    }
}
