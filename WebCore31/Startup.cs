using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCore31.Extentions;
using WebCore31.SettingModels;
using System.IO;
using Microsoft.Extensions.FileProviders;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Reflection;

namespace WebCore31
{
    //框架里面的约定，可以用方法名称来匹配在环境变量中配置的环境名称
    //比如在launchSettings.json中配置名为"demo"，则会找到ConfigureDemoServices的方法 和 ConfigureDemo
    //还可以使用类名匹配，比如再加个StartupDemo的类，则需要在Program.cs中将
    //webBuilder.UseStartup<Startup>(); 改成 webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        /*
        /// <summary>
        /// 多环境的方法
        /// 如果lauchSettings.json中的环境变量自己命名为Demo的时候，配置服务的时候会找名字匹配的方法名称
        /// 所以会执行下面的ConfigureDemoServices而不会执行ConfigureServies
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDemoServices(IServiceCollection services)
        {

        }
        */
        //默认的。如果在环境变量里面没有找到匹配的环境名称方法，就执行此方法
        public void ConfigureServices(IServiceCollection services)
        {
            //添加对AutoMapper的支持，会查找所有程序集中继承了 Profile 的类
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            /*
            //注入方法2
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile.MapperProfile());
            }, new Type[] { });
            */

            /*
            //注入方法3
            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile.MapperProfile());
            }).CreateMapper());
            */            
            //相当于api
            services.AddControllers();
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            //这里是2.X里面的写法，在3.X版本里面可以拆分成上面2个
            //services.AddMvc();

            services.AddMessage(builder => builder.UseEmail());

            //注册配置选项的服务
            services.Configure<AppSetting>(_configuration);
            /*
            string rootpath = _env.ContentRootPath;
            //读取自定义文件
            string path = Path.Combine(rootpath, "diy.json");
            var config = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                .AddJsonFile(path)
                .Build();
            //注册自定义的配置文件服务
            //services.Configure<classname>(config);
            string s = config["name"];

            */
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My Demo API", Version = "V1"});
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebCore31.xml"), true);
            });
        }

        /*
        //如果环境变量中配置了Demo的环境，则配置中间件的方法
        public void ConfigureDemo(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
        */
        //默认的配置管道的方法
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSetting> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();

            var setting = new AppSetting();
            //配置文件整个绑定到对象
            _configuration.Bind(setting);

            //配置文件部分绑定到对象
            var loglevel = new Loglevel();
            _configuration.GetSection("Logging").GetSection("Loglevel").Bind(loglevel);

            /*
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello use 1 begin \r\n");
                await next();
                await context.Response.WriteAsync("hello use 1 end \r\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello use 2 begin \r\n");
                await next();
                await context.Response.WriteAsync("hello use 2 end \r\n");
            });
            */
            //自定义中间件的约定 
            //1.具有RequestDelegate参数的构造函数
            //2.具有返回类型为Task的Invoke或者InvokeAsync的方法，且具有HttpContext参数
            //app.UseMiddleware<TestMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Demo API V1"));

            //获取路由信息，包括路由数据（路由模板）、获取对应的中间件
            app.UseRouting();
            
            //.net core 2.X里面没有这个
            //3.X里面拆开出来为了复用

            /*
            app.Use(async (context, next) =>
            {
                var ep = context.GetEndpoint();
                var rv = context.Request.RouteValues;
                //await ep.RequestDelegate(context);
                await next();
            });
            */
            //终结点中间件
            app.UseEndpoints(endpoints =>
            {
                ////RazorPages路由
                //endpoints.MapRazorPages();
                ////特性路由(WebApi)
                endpoints.MapControllers();
                //控制器路由(MVC)
                endpoints.MapControllerRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapGet("/test", async context =>
                {
                    await context.Response.WriteAsync("Hello World!\r\n");
                });
            });
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("end");
            //});
        }
    }
}
