using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new ApplicationBuilder();
            //app.Use(x =>
            //{
            //    Console.WriteLine("中间件0号 begin");
            //    return async context => { await Task.Run(() => {
            //        Console.WriteLine("hello");
            //    }); };
            //});
            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件1号 begin");
                await next();
                Console.WriteLine("中间件1号 end");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件2号 begin");
                await next();
                Console.WriteLine("中间件2号 end");
            });
            var firstmiddleware = app.Build();
            firstmiddleware(new HttpContext());
            Console.ReadLine();
        }
    }
}
