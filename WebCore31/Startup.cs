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
    //��������Լ���������÷���������ƥ���ڻ������������õĻ�������
    //������launchSettings.json��������Ϊ"demo"������ҵ�ConfigureDemoServices�ķ��� �� ConfigureDemo
    //������ʹ������ƥ�䣬�����ټӸ�StartupDemo���࣬����Ҫ��Program.cs�н�
    //webBuilder.UseStartup<Startup>(); �ĳ� webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);

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
        /// �໷���ķ���
        /// ���lauchSettings.json�еĻ��������Լ�����ΪDemo��ʱ�����÷����ʱ���������ƥ��ķ�������
        /// ���Ի�ִ�������ConfigureDemoServices������ִ��ConfigureServies
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDemoServices(IServiceCollection services)
        {

        }
        */
        //Ĭ�ϵġ�����ڻ�����������û���ҵ�ƥ��Ļ������Ʒ�������ִ�д˷���
        public void ConfigureServices(IServiceCollection services)
        {
            //��Ӷ�AutoMapper��֧�֣���������г����м̳��� Profile ����
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            /*
            //ע�뷽��2
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile.MapperProfile());
            }, new Type[] { });
            */

            /*
            //ע�뷽��3
            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile.MapperProfile());
            }).CreateMapper());
            */            
            //�൱��api
            services.AddControllers();
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            //������2.X�����д������3.X�汾������Բ�ֳ�����2��
            //services.AddMvc();

            services.AddMessage(builder => builder.UseEmail());

            //ע������ѡ��ķ���
            services.Configure<AppSetting>(_configuration);
            /*
            string rootpath = _env.ContentRootPath;
            //��ȡ�Զ����ļ�
            string path = Path.Combine(rootpath, "diy.json");
            var config = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                .AddJsonFile(path)
                .Build();
            //ע���Զ���������ļ�����
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
        //�������������������Demo�Ļ������������м���ķ���
        public void ConfigureDemo(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
        */
        //Ĭ�ϵ����ùܵ��ķ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSetting> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();

            var setting = new AppSetting();
            //�����ļ������󶨵�����
            _configuration.Bind(setting);

            //�����ļ����ְ󶨵�����
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
            //�Զ����м����Լ�� 
            //1.����RequestDelegate�����Ĺ��캯��
            //2.���з�������ΪTask��Invoke����InvokeAsync�ķ������Ҿ���HttpContext����
            //app.UseMiddleware<TestMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Demo API V1"));

            //��ȡ·����Ϣ������·�����ݣ�·��ģ�壩����ȡ��Ӧ���м��
            app.UseRouting();
            
            //.net core 2.X����û�����
            //3.X����𿪳���Ϊ�˸���

            /*
            app.Use(async (context, next) =>
            {
                var ep = context.GetEndpoint();
                var rv = context.Request.RouteValues;
                //await ep.RequestDelegate(context);
                await next();
            });
            */
            //�ս���м��
            app.UseEndpoints(endpoints =>
            {
                ////RazorPages·��
                //endpoints.MapRazorPages();
                ////����·��(WebApi)
                endpoints.MapControllers();
                //������·��(MVC)
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
