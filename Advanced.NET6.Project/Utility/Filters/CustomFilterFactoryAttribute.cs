using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced.NET6.Project.Utility.Filters
{
    public class CustomFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private Type _Type;

        public CustomFilterFactoryAttribute(Type type)
        { 
          this._Type=type;
        }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return (IFilterMetadata)serviceProvider.GetService(_Type);
        }
    }
}
