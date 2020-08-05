using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebCore31.Services;

namespace WebCore31.Extentions
{
    public class MessageServiceBuilder
    {
        public IServiceCollection ServiceCollection { get; set; }
        public MessageServiceBuilder(IServiceCollection services)
        {
            ServiceCollection = services;
        }

        public void UseEmail()
        {
            ServiceCollection.AddSingleton<IMessage, EmailMessage>();
        }

        public void UseSms()
        {
            ServiceCollection.AddSingleton<IMessage, SmsMessage>();
        }
    }
}
