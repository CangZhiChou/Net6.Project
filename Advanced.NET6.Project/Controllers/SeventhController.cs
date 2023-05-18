using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Framework.AutofaExt;
using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.Project.Controllers
{
    public class SeventhController : Controller
    {

        private IPhone _IPhone;

        public SeventhController(IPhone phone)
        {
            this._IPhone = phone;
        }

        public IActionResult Index()
        { 
            //_IPhone.Text();
            //_IPhone.Call();
            object oInstance = _IPhone.QueryUser(new { QueryId = 123, QueryName = "朝夕教育" });
            ViewBag.Data = Newtonsoft.Json.JsonConvert.SerializeObject(oInstance);
            return View();
        }

        [HttpGet] 
        public object GetData()
        {
            return new
            {
                Success = true,
                Mesaage = "OK"
            };
        }
    }
}
