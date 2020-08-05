using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBuilder
{
    public delegate Task RequestDelegate(HttpContext context);
    class ApplicationBuilder
    {
        private static readonly IList<Func<RequestDelegate, RequestDelegate>> _components =
            new List<Func<RequestDelegate, RequestDelegate>>();

        //扩展use方法
        public ApplicationBuilder Use(Func<HttpContext, Func<Task>, Task> middleware)
        {
            return Use(next =>
            {
                return context =>
                {
                    Task task() => next(context);
                    return middleware(context, task);
                };
            });
        }

        //原生use方法
        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                Console.WriteLine("默认中间件");
                return Task.CompletedTask;
            };

            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }
            return app;
        }
    }
}
