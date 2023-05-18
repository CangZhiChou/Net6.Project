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
            Title = $"{field.Name}:�����ǳ�ϦAdvanced~",
            Version = field.Name,
            Description = $"coreWebApi {field.Name} �汾"
        });
    }

    #region ΪSwagger JSON and UI����xml�ĵ�ע��·�� 
    string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
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

#region ��Ȩ��Ȩ
app.UseAuthentication();
app.UseAuthorization();
#endregion


app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
