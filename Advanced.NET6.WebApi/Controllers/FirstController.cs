using Advanced.NET6.WebApi.Utility;
using Advanced.NET6.WebApi.Utility.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class FirstController : ControllerBase
    { 
        private readonly ILogger<FirstController> _logger;

        public FirstController(ILogger<FirstController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetData")] 
        //[CustomCorsActionFilterAttribute]
        public object GetData()
        {
            //HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
            Console.WriteLine("FirstController-GetData-������~~");
            return new
            {
                Id = 123,
                Name = "Richard"
            };
        }

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Route("PostData")]
        public object PostData()
        {
            return new
            {
               Success=true,
               Mesaage="OK"
            };
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        //[Route("PutData")]
        public object PutData()
        {
            return new
            {
                Success = true,
                Mesaage = "OK"
            };
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        //[Route("DeleteData")]
        public object DeleteData()
        {
            return new
            {
                Success = true,
                Mesaage = "OK"
            };
        }
    }
}