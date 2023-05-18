
using Advanced.NET6.Project.Models;
using Advanced.NET6.Project.Utility.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Advanced.NET6.ExceptionService;

namespace Advanced.NET6.Project.Controllers
{
    [CustomCacheResourceFilter] //2.对当前的控制器下的所有的Action都可以生效的
    public class ThirdController : Controller
    {
        private readonly ILogger<ThirdController> _Logger;
        private readonly ILoggerFactory _LoggerFactory;
        public ThirdController(ILogger<ThirdController> logger, ILoggerFactory loggerFactory)
        {

            //throw new Exception("控制器发生异常了。。");

            this._Logger = logger;
            this._Logger.LogInformation($"{this.GetType().Name} 被构造了。。。_Logger");

            this._LoggerFactory = loggerFactory;
            ILogger<ThirdController> _Logger2 = this._LoggerFactory.CreateLogger<ThirdController>();
            _Logger2.LogInformation($"{this.GetType().Name} 被构造了。。。_Logger2");

            Console.WriteLine($"{this.GetType().FullName} 被构造。。。。。");
        }

        #region ResourceFilter 
        [CustomCacheResourceFilter]
        // [CustomCacheActionFilter]
        public IActionResult Index()
        {
            //1.定义一个缓存的区域
            //2.请求来了，根据缓存的标识---判断缓存如果有缓存，就返回缓存的值
            //3.如果没有缓存---做计算
            //4.计算结果保存到缓存中去
            {
                //这里有业务逻辑---调用业务逻辑计算的结果
            }
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd   ss");
            return View();
        }


        [CustomCacheAsyncResourceFilter]
        public IActionResult Index1()
        {
            {
                //支持缓存
            }
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd   ss");
            Console.WriteLine("这里是Index1 方法被执行了。。。");
            return View();
        }
        #endregion

        #region ActionFilter 

        //[CustomLogActionFilter]   //知识点：IOC容器的问题--后面的视频中会有详细讲解
        //[TypeFilter(typeof(CustomLogActionFilterAttribute))]
        // [ServiceFilter(typeof(CustomLogActionFilterAttribute))]   //还需要注册服务
        //[TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
        [CustomFilterFactory(typeof(CustomLogAsyncActionFilterAttribute))]
        public IActionResult Index2(int id)
        {
            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = id,
                Name = "Richard--ViewBag",
                Age = 34
            });

            ViewData["UserInfo"] = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Id = id,
                Name = "Richard --ViewData",
                Age = 34
            });

            object description = "欢迎大家来到Richard老师的视频课";
            return View(description);
        }
        #endregion

        #region ResultFilter

        //[CustomResultFilter]

        [AllowAnonymousAttribute]
        public IActionResult Index3()
        {
            return View();
        }

        //[CustomResultFilter]
        //[CustomAsyncResultFilter]
        [TypeFilter(typeof(CustomAllActionResultFilterAttribute))]
        public IActionResult Index4(int i)
        {
            //return Json(new AjaxResult()
            //{

            //});

            return Json(new
            {
                Id = 123,
                Name = "Richard",
                Age = 34
            });
        }
        #endregion

        #region CustomAlwaysRunResultFilter

        [CustomCacheResourceFilter] //1.仅仅对当前的Action生效
        //[CustomAlwaysRunResultFilter]
        public IActionResult Index5()
        {
            return Json(new
            {
                Id = 123,
                Name = "Richard",
                Age = 34
            });
        }
        #endregion

        #region 匿名支持

        /// <summary>
        /// 不希望你支持缓存怎么做呢、
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymousAttribute]
        [CustomAllowAnonymousAttribute]
        public IActionResult Index6()
        {
            return Json(new
            {
                Id = 123,
                Name = "Richard",
                Age = 34
            });
        }
        #endregion

        #region ExceptionFilter

        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index7()
        {
            throw new Exception("其实没啥，就是想要异常一下~~");
        }
        #endregion


        #region ExceptionFilter能够捕捉到的异常
        //1. Action出现没有处理的异常
        //2. Action出现已经处理的异常
        //3. Service层的异常      
        //4. View绑定时出现了异常    
        //5. 不存在的Url地址        
        //6. 其他Filter中发生的异常
        //7. 控制器构造函数出现异常 

        /// <summary>
        ///  Action出现没有处理的异常
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index8()
        {
            throw new Exception(" Action出现没有处理的异常");
        }

        /// <summary>
        ///  Action出现已经处理的异常
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index9()
        {
            try
            {
                throw new Exception(" Action出现已经处理的异常");
            }
            catch (Exception)
            {
                return View();
            }
        }

        /// <summary>
        /// Service层的异常      
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index10()
        {
            new ExceptionInFoService().Show();
            return View();
        }

        /// <summary>
        /// View绑定时出现了异常    
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index11()
        {
            return View();
        }
         
        /// <summary>
        ///其他Filter中发生的异常
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
     /*  [TypeFilter(typeof(CustomLogActionFilterAttribute))] */  //Action中发生异常---可以捕捉到
       // [TypeFilter(typeof(CustomCacheResourceFilterAttribute))]   //Resource中发生异常--捕捉不到
        [TypeFilter(typeof(CustomResultFilterAttribute))]   //Result中发生异常 ---捕捉不到的
        public IActionResult Index12()
        {
            return View();
        }


        #endregion
    }
}
