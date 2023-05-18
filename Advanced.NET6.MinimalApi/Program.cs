using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Business.Services;
using Advanced.NET6.MinimalApi;
using Advanced.NET6.MinimalApi.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

{
    builder.Services.AddTransient<IMicrophone, Microphone>();
    builder.Services.AddTransient<IPower, Power>();
    builder.Services.AddTransient<IHeadphone, Headphone>();
    builder.Services.AddTransient<IPhone, ApplePhone>();
}

#region jwt校验  HS
{
    //第二步，增加鉴权逻辑
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
         {
         options.TokenValidationParameters = new TokenValidationParameters
         {
                 //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                 ValidateIssuer = true,//是否验证Issuer
                 ValidateAudience = true,//是否验证Audience
                 ValidateLifetime = true,//是否验证失效时间
                 ValidateIssuerSigningKey = true,//是否验证SecurityKey
                 ValidAudience = tokenOptions.Audience,//
                 ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey 
             };
     });

    //配置授权
    builder.Services.AddAuthorization();

}
#endregion


var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); //使用https


#region 鉴权授权
app.UseAuthentication();
app.UseAuthorization();
#endregion




var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.OrderExtension(); //订单的api
app.ProductExtension();


app.MapPost("/Test", () =>
{
    return new
    {
        Success = true,
        Message = "新增操作成功"
    };
}).WithTags("Test")
.RequireAuthorization();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}