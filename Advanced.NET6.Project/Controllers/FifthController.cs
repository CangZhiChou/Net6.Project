using Advanced.NET6.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.Project.Controllers
{
    public class FifthController : Controller
    {

        private readonly IMicrophone _IMicrophone;
        private readonly IMicrophone _IMicrophone2;
        public FifthController(IMicrophone iMicrophone, IServiceProvider serviceProvider)
        {
            this._IMicrophone = iMicrophone;
            _IMicrophone2 = serviceProvider.GetService<IMicrophone>(); 
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
