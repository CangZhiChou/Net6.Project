using Advanced.NET6.WebApi.Utility;
using Advanced.NET6.WebApi.Utility.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
    {
        c.SwaggerDoc(field.Name, new OpenApiInfo()
        {
            Title = $"{field.Name}:这里是朝夕Advanced~",
            Version = field.Name,
            Description = $"coreWebApi {field.Name} 版本"
        });
    }

    #region 为Swagger JSON and UI设置xml文档注释路径 
    string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
    string xmlPath = Path.Combine(basePath, "Advanced.NET6.WebApi.xml");
    c.IncludeXmlComments(xmlPath);
    #endregion
});
builder.Services.AddCors(policy =>
{
policy.AddPolicy("CorsPolicy", opt => opt
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
.WithExposedHeaders("X-Pagination"));
});


{
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
    }
    #endregion
}


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
    {
        c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
    }
});

//app.UseHttpsRedirection();

#region 鉴权授权
app.UseAuthentication();
app.UseAuthorization();
#endregion


app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
