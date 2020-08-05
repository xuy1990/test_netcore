using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebCore31
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);
                    //webBuilder.UseStartup<Startup>();

                    //配置端口的几种方式
                    //优先级： 环境变量< 硬编码 < 应用配置 < 命令行
                    //环境变量以ASPNETCORE_URLS  在lauchSettings.json里面
                    //硬编码如以下这句
                    //webBuilder.UseUrls("http://localhost:8000");
                    //应用配置端口方式  在 appsettings.json文件夹中的 urls节点
                    //命令行配置端口方式   在内容文件夹下 输入：dotnet run --urls "http://localhost:9000"


                    //配置日志
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Information));

                    //修改静态文件根目录，默认为wwwroot，还有一种方法是在app.UseStaticFiles()方法里面配置
                    //webBuilder.UseWebRoot("www");
                });
    }
}
