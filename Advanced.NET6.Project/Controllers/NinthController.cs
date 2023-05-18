using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Business.Services;
using Advanced.NET6.EFCore.DB.Models;
using Advanced.NET6.Framework;
using Advanced.NET6.Framework.RazorExtension;
using Advanced.NET6.Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Advanced.NET6.Project.Controllers
{
    public class NinthController : Controller
    {
        private readonly ILogger<NinthController> _logger;

        private readonly ICommodityService _ICommodityService;
        private readonly ICompanyService _ICompanyService;

        public NinthController(ILogger<NinthController> logger, ICommodityService iCommodityService, ICompanyService iCompanyService)
        {
            _logger = logger;
            this._ICommodityService = iCommodityService;
            this._ICompanyService = iCompanyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回视图的时候，也返回数据
        /// </summary>
        /// <returns></returns>
        public IActionResult ListView(string searchString, string url, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Commodity, bool>> expression = c => true;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                expression = c => c.Title.Contains(searchString);
            }
            PageResult<Commodity> pageResult = _ICommodityService.QueryPage<Commodity, int>(expression, pageSize, pageIndex, c => c.Id, false);

            PagingData<Commodity> result = new PagingData<Commodity>()
            {
                DataList = pageResult.DataList,
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                RecordCount = pageResult.TotalCount,
                SearchString = searchString
            };
            return View(result);
        }

        public IActionResult RazorExt()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> selectListItems = GetCompanyList();
            ViewBag.categoryList = selectListItems;
            return View(new Commodity());
        }

        [HttpPost]
        public IActionResult Create(Commodity commodity)
        { 
            if (ModelState.IsValid)
            {
                _ICommodityService.Insert<Commodity>(commodity);
                return new RedirectResult("/Ninth/ListView");
            }
            else
            {
                List<SelectListItem> selectListItems = GetCompanyList();
                ViewBag.categoryList = selectListItems;
                return View(commodity);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> selectListItems = GetCompanyList();
            ViewBag.categoryList = selectListItems;
            Commodity commodity = _ICommodityService.Find<Commodity>(id);
            return View(commodity);
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetCompanyList()
        {
            List<SelectListItem> selectListItems = _ICompanyService.Set<CompanyInfo>().Select(c => new SelectListItem()
            {
                Text = c.CompanyName,
                Value = c.Id.ToString()
            }).ToList();


            selectListItems.Add(new SelectListItem()
            {
                Selected = true,
                Value = "",
                Text = "------请选择---------"
            });
            return selectListItems;
        }

        [HttpPost]
        public IActionResult Edit(Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                _ICommodityService.Update(commodity);
                return new RedirectResult("/Ninth/ListView");
            }
            else
            {
                List<SelectListItem> selectListItems = GetCompanyList();
                ViewBag.categoryList = selectListItems;
                return View(commodity);
            } 
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            _ICommodityService.Delete<Commodity>(id);
            return Redirect("/Ninth/ListView");
        }



        [HttpGet]
        public IActionResult AjaxDelete(int id)
        {
            _ICommodityService.Delete<Commodity>(id);
            return Json(new
            {
                result = 1,
                message = "操作成功"
            });
        }


    }
}