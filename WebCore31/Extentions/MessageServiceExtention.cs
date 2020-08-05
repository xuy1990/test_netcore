using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore31.Services;

namespace WebCore31.Extentions
{
    public static class MessageServiceExtention
    {
        public static void AddMessage(this IServiceCollection services)
        {
            MessageServiceBuilder builder = new MessageServiceBuilder(services);
            builder.UseEmail();
        }

        public static void AddMessage(this IServiceCollection services, Action<MessageServiceBuilder> configure)
        {
            var builder = new MessageServiceBuilder(services);
            configure(builder);
        }
    }
}
