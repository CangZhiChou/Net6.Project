using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Framework.AutofaExt
{
    public class CusotmPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            //在这里就是判断哪些属性是需要做属性注入的
            return propertyInfo.CustomAttributes.Any(c => c.AttributeType == typeof(CusotmSelectAttribute));
        }
    }
}
