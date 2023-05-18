using Advanced.NET6.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.NET6.WebApi.Controllers
{
    [ApiController]
    [Route("V2/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V2))]
    public class FirstV2Controller : ControllerBase
    { 
        private readonly ILogger<FirstV2Controller> _logger;

        public FirstV2Controller(ILogger<FirstV2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[Route("GetData")]
        public object GetData()
        {
            return new
            {
                Id = 123,
                Name = "Richard"
            };
        }

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