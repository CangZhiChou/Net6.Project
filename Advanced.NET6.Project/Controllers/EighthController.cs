using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Business.Services;
using Advanced.NET6.EFCore.DB.Models;
using Advanced.NET6.Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Advanced.NET6.Project.Controllers
{
    public class EighthController : Controller
    {
        private readonly ILogger<EighthController> _logger;

        private readonly ICommodityService _ICommodityService;


        public EighthController(ILogger<EighthController> logger, ICommodityService iCommodityService)
        {
            _logger = logger;
            this._ICommodityService = iCommodityService;
        }

        /// <summary>
        /// 关于分层：
        /// 1.UI层：Advanced.NET6.Project
        /// 2.数据访问层：Advanced.NET6.EFCore.DB
        /// 3.缺：业务逻辑层：
        ///       Advanced.NET6.Business.Services
        ///       
        /// 案例：查询Commodity--UI--业务逻辑层--数据访问层
        ///     
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //CustomerDbContext context = new CustomerDbContext();
            //Commodity commodity = context.Commodities.FirstOrDefault(); 
            //ICommodityService commodityService = new CommodityService();
            //commodityService.Query();
            //commodityService.Update();
            ////....
            /// 
            //ICommodityService commodityService = new CommodityService(new CustomerDbContext()); 
            //List<Commodity> commodities = commodityService.Query<Commodity>(c => true).ToList();


            List<Commodity> commodities = _ICommodityService.Query<Commodity>(c => true).ToList(); 
            return View(commodities);
        }

    }
}