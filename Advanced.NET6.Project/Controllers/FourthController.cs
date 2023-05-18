
using Advanced.NET6.Project.Models;
using Advanced.NET6.Project.Utility.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Advanced.NET6.ExceptionService;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Advanced.NET6.Project.Utility.Filters.FilterTest;

namespace Advanced.NET6.Project.Controllers
{
    [CustomControllerActionFilterAttribute(Order =20)]
    public class FourthController : Controller
    {
        private readonly ILogger<FourthController> _Logger;
        private readonly ILoggerFactory _LoggerFactory;
        public FourthController(ILogger<FourthController> logger, ILoggerFactory loggerFactory)
        {
            this._Logger = logger;
            this._LoggerFactory = loggerFactory;
            ILogger<FourthController> _Logger2 = this._LoggerFactory.CreateLogger<FourthController>();
        }


        /// <summary>
        /// 如果这里是一个数据列表
        /// 部分人能看---部分人是不能看
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")] //如果仅仅是这样标记：授权是对标于默认授权渠道
        public IActionResult Index()
        { 
            var user = HttpContext.User; 
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "User")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Teach")]
        public IActionResult Index1()
        {
            return View();
        }

        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin,User")]  //用户信息中是包含两个角色的

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin,Teache")]  //用户信息中是只包含一个Admin
        public IActionResult Index2()
        {
            return View();
        }


        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Policy = "rolePolicy")]  //用户信息中是只包含一个Admin
        
        public IActionResult Index3()
        {
            return View();
        }

        /// <summary>
        /// 请求来了以后，我希望拿到用户的信息---去到数据库去比对一下，或者请求第三方服务器去验证一下。。。如果验证通过就允许请求该方法，如果验证不通过，就不允许调用这个方法；
        /// </summary>
        /// <returns></returns>
        public IActionResult Index4()
        {
            return View();
        }


        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "rolePolicy")]
        [CustomCacheResourceFilter]
        [TypeFilter(typeof(CustomLogActionFilterAttribute))]
        [TypeFilter(typeof(CustomResultFilterAttribute))]
        [TypeFilter(typeof(CustomAlwaysRunResultFilterAttribute))]
        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index5()
        {
            return View();
        }

        [CustomActionActionFilterAttribute(Order =-99)]
        public IActionResult Index6()
        {
            return View();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            if ("Richard".Equals(name) && "1".Equals(password))
            {
                var claims = new List<Claim>()//鉴别你是谁，相关信息
                    {
                        new Claim("Userid","1"),
                        new Claim(ClaimTypes.Role,"Admin"),
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim(ClaimTypes.Name,$"{name}--来自于Cookies"),
                        new Claim(ClaimTypes.Email,$"18672713698@163.com"),
                        new Claim("password",password),//可以写入任意数据
                        new Claim("Account","Administrator"),
                        new Claim("role","admin"),
                         new Claim("QQ","1030499676")
                    };
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),//过期时间：30分钟

                }).Wait();
                var user = HttpContext.User;
                return base.Redirect("/Fourth/Index");
            }
            else
            {
                base.ViewBag.Msg = "用户或密码错误";
            }
            return await Task.FromResult<IActionResult>(View());
        }


    }
}
