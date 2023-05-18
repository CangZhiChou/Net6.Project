using Advanced.NET6.MinimalApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Advanced.NET6.MinimalApi
{
    public static class ProductMinimalApiExt
    {
        public static void ProductExtension(this WebApplication app)
        {
            app.MapGet("/QueryProduct", () =>
            {
                return new
                {
                    Id = 123,
                    Name = "Richrd",
                    Age = 35
                };
            })
           .WithTags("Product");

            app.MapPost("/AddProduct", () =>
            {
                return new
                {
                    Success = true,
                    Message = "新增操作成功"
                };
            })
            .WithTags("Product");

            app.MapPut("/UpdaetProduct", () =>
            {
                return new
                {
                    Success = true,
                    Message = "修改操作成功"
                };
            })
            .WithTags("Product");

            app.MapDelete("/DeleteProduct", () =>
            {
                return new
                {
                    Success = true,
                    Message = "删除操作成功"
                };
            }) 
            .WithTags("Product");

        }
    }
}
