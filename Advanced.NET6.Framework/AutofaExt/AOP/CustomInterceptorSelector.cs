using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Framework.AutofaExt.AOP
{
    public class CustomInterceptorSelector : IInterceptorSelector
    {
        //private CusotmLogInterceptor _CusotmLogInterceptor;
        //public CustomInterceptorSelector(CusotmLogInterceptor cusotmLogInterceptor)
        //{
        //    this._CusotmLogInterceptor = cusotmLogInterceptor;
        //}

        /// <summary>
        /// 让我们选择使用哪个IInterceptor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //return interceptors;
            return new IInterceptor[] { 
                new CusotmCacheInterceptor(),
                new CusotmInterceptor()
                 //_CusotmLogInterceptor
            };
        }
    }
}
