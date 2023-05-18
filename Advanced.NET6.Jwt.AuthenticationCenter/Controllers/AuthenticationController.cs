using Advanced.NET6.Jwt.AuthenticationCenter.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace Advanced.NET6.Jwt.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ICustomJWTService _iJWTService = null;
        public AuthenticationController(ICustomJWTService customJWTService)
        {
            _iJWTService = customJWTService;
        }
         
        [Route("Get")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int>() { 1, 2, 3, 4, 6, 7 };
        }

        [Route("Login")]
        [HttpPost]
        public string Login(string name, string password)
        {
            //在这里需要去数据库中做数据验证
            if ("Richard".Equals(name) && "123456".Equals(password))
            {
                //就应该生成Token 
                string token = this._iJWTService.GetToken(name, password);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });

            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }
    }
}
