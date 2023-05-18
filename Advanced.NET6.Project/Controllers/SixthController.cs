using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Framework.AutofaExt;
using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.Project.Controllers
{
    public class SixthController : Controller
    {
        private IMicrophone _IMicrophone;
        private IPhone _IPhone;
        IEnumerable<IMicrophone> _IMicrophonelist;
         
       [CusotmSelectAttribute]
        public IMicrophone _IMicrophoneProp { get; set; }
         
        public IMicrophone _IMicrophoneProp1 { get; set; }

        public SixthController(IMicrophone microphone, IPhone phone, IEnumerable<IMicrophone> list, IServiceProvider serviceProvider, IComponentContext componentContext)
        {
            this._IMicrophone = microphone;
            this._IPhone = phone;
            this._IMicrophonelist = list;
            IMicrophone microphone1 = serviceProvider.GetService<IMicrophone>();
            IMicrophone microphone2 = componentContext.Resolve<IMicrophone>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
