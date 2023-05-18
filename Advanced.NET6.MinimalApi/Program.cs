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

#region jwtУ��  HS
{
    //�ڶ��������Ӽ�Ȩ�߼�
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     .AddJwtBearer(options =>  //���������õļ�Ȩ���߼�
         {
         options.TokenValidationParameters = new TokenValidationParameters
         {
                 //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
                 ValidateIssuer = true,//�Ƿ���֤Issuer
                 ValidateAudience = true,//�Ƿ���֤Audience
                 ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                 ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                 ValidAudience = tokenOptions.Audience,//
                 ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//�õ�SecurityKey 
             };
     });

    //������Ȩ
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

//app.UseHttpsRedirection(); //ʹ��https


#region ��Ȩ��Ȩ
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

app.OrderExtension(); //������api
app.ProductExtension();


app.MapPost("/Test", () =>
{
    return new
    {
        Success = true,
        Message = "���������ɹ�"
    };
}).WithTags("Test")
.RequireAuthorization();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}