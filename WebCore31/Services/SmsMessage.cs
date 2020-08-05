using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore31.Services
{
    public class SmsMessage : IMessage
    {
        public string Send()
        {
            return "Sms";
        }
    }
}
