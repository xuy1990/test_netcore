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

                    //���ö˿ڵļ��ַ�ʽ
                    //���ȼ��� ��������< Ӳ���� < Ӧ������ < ������
                    //����������ASPNETCORE_URLS  ��lauchSettings.json����
                    //Ӳ�������������
                    //webBuilder.UseUrls("http://localhost:8000");
                    //Ӧ�����ö˿ڷ�ʽ  �� appsettings.json�ļ����е� urls�ڵ�
                    //���������ö˿ڷ�ʽ   �������ļ����� ���룺dotnet run --urls "http://localhost:9000"


                    //������־
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Information));

                    //�޸ľ�̬�ļ���Ŀ¼��Ĭ��Ϊwwwroot������һ�ַ�������app.UseStaticFiles()������������
                    //webBuilder.UseWebRoot("www");
                });
    }
}
