using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.MinimalApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Advanced.NET6.MinimalApi
{
    public static class OrderMinimalApiExt
    {
        public static void OrderExtension(this WebApplication app)
        {
            app.MapGet("/QueryOrder", (int id, HttpContext context, IConfiguration configuration, IMicrophone microphone, IPhone phone) =>
            {
                //参数---注入
                //上下文---注入
                //Servcie服务
                var query = context.Request.Query;
                var microphone1 = microphone;
                var phone1 = phone;
                var configuration1 = configuration;
                return new
                {
                    Id = 123,
                    Name = "Richrd",
                    Age = 35
                };
            })
           .WithTags("Order")
           .RequireAuthorization(new CustomAuthorizeData()
           {
               AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = "teache0"
           });

            app.MapPost("/AddOrder", () =>
            {
                return new
                {
                    Success = true,
                    Message = "新增操作成功"
                };
            })
            .WithTags("Order");

            app.MapPut("/UpdaetOrder", () =>
            {
                return new
                {
                    Success = true,
                    Message = "修改操作成功"
                };
            })
            .WithTags("Order");

            app.MapDelete("/DeleteOrder", () =>
            {
                return new
                {
                    Success = true,
                    Message = "删除操作成功"
                };
            })
            .WithTags("Order");

        }
    }
}
