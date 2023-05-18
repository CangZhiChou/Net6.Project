using Advanced.NET6.Business.Interfaces;
using Advanced.NET6.Business.Services;
using Advanced.NET6.EFCore.DB.Models;
using Advanced.NET6.ExceptionService;
using Advanced.NET6.Framework.AutofaExt;
using Advanced.NET6.Framework.AutofaExt.AOP;
using Advanced.NET6.Project.Utility;
using Advanced.NET6.Project.Utility.Filters;
using Advanced.NET6.Project.Utility.Filters.FilterTest;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using NLog.Web;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region log4net
{
    ////Nuget���룺
    ////1.Log4Net
    ////2.Microsoft.Extensions.Logging.Log4Net.AspNetCore
    builder.Logging.AddLog4Net("CfgFile/log4net.Config");
}
#endregion

#region NLogin
{
    //Nuget���룺NLog.Web.AspNetCore
    //builder.Logging.AddNLog("CfgFile/NLog.config");
}
#endregion

#region ʹ��Session
builder.Services.AddSession();
#endregion

#region ʹ��Autofac
{
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>((context, buider) =>
    {
        buider.RegisterType<Microphone>().As<IMicrophone>();
        buider.RegisterType<CommodityService>().As<ICommodityService>();
        buider.RegisterType<CompanyService>().As<ICompanyService>();
        //buider.RegisterType<CustomerDbContext>().As<DbContext>(); 
    });
}
#endregion 
#region EFCore6 ����ASP.NET Core6.0
builder.Services.AddTransient<DbContext, CustomerDbContext>();
#endregion


// Add services to the container.
builder.Services.AddControllersWithViews(mvcOptions =>
{
    mvcOptions.Filters.Add<CustomGlobalActionFilterAttribute>();
    //mvcOptions.Filters.Add<CustomCacheResourceFilterAttribute>(); //3.ȫ��ע��--��������Ŀ����Ч��
});

#region ���ü�Ȩ
{
    //ѡ��ʹ�����ַ�ʽ����Ȩ
    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
    {
        option.LoginPath = "/Fourth/Login";//���û���ҵ��û���Ϣ---��Ȩʧ��--��ȨҲʧ����---����ת��ָ����Action
        option.AccessDeniedPath = "/Home/NoAuthority";
    });
}
#endregion

#region ������Ȩ
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("rolePolicy", policyBuilder =>
        {
            //policyBuilder.RequireRole("Teacher"); 
            //policyBuilder.RequireClaim("Account");//�������ĳһ��Claim

            policyBuilder.RequireAssertion(context =>
            {
                bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                   && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin"
                   && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);

                //UserService userService = new UserService();
                ////userService.Validata(); 
                return bResult;
            });

            policyBuilder.AddRequirements(new QQEmailRequirement());

        });
    });
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IAuthorizationHandler, QQHandler>();
}
#endregion

#region ע�����
{
    //builder.Services.AddTransient<IMicrophone, Microphone>();
    //builder.Services.AddTransient<IPower, Power>();

    builder.Services.AddTransient<CustomLogAsyncActionFilterAttribute>();
}
#endregion

#region Autfaoc����ASP.NET Core
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//ͨ�������滻����Autofac���Ͻ���

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
    containerBuilder.RegisterType<Power>().As<IPower>();
    containerBuilder.RegisterType<Headphone>().As<IHeadphone>();
    containerBuilder.RegisterType<ApplePhone>().As<IPhone>()
    //.EnableClassInterceptors(); //��ǰ������ Ҫ֧��AOP��չ--ͨ������֧��
    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
    {
        Selector = new CustomInterceptorSelector()
    });
    containerBuilder.RegisterType<CusotmInterceptor>();
    containerBuilder.RegisterType<CusotmLogInterceptor>();
    containerBuilder.RegisterType<CusotmCacheInterceptor>();


    //ע��ÿ���������ͳ���֮��Ĺ�ϵ
    var controllerBaseType = typeof(ControllerBase);
    containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
        .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
        .PropertiesAutowired(new CusotmPropertySelector()); //֧������ע��

});
#endregion

#region MyRegion
{
    //֧�ֿ�������ʵ����IOC����������---autofac������
    builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
}
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

#region �м�������쳣
{
    ///���Http�����е�Response�е�״̬����200,�ͻ����Home/Error�У�
    app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");//ֻҪ����200 ���ܽ���

    //����������Լ�ƴװһ��Reponse ���
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            await context.Response.WriteAsync("ERROR!<br><br>\r\n");
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            Console.WriteLine($"{exceptionHandlerPathFeature?.Error.Message}");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
            }
            await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
            await context.Response.WriteAsync("</body></html>\r\n");
            await context.Response.WriteAsync(new string(' ', 512)); // IE padding
        });
    });
}
#endregion

app.UseSession();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();//��Ȩ
app.UseAuthorization(); //��Ȩ

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
