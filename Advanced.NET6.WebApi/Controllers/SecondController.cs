using Advanced.NET6.WebApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class SecondController : ControllerBase
    { 
        private readonly ILogger<SecondController> _logger;

        public SecondController(ILogger<SecondController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetData")] 
        [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
        public object GetData()
        {
            Console.WriteLine("������~~");
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