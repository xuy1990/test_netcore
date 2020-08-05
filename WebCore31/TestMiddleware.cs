using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebCore31
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(ms))
                {
                    writer.WriteStartObject();
                    writer.WriteString("name", "hh");
                    writer.WriteNumber("id", 1);
                    writer.WriteEndObject();
                    writer.Flush();
                }
                await context.Response.WriteAsync(Encoding.UTF8.GetString(ms.ToArray()) + "\r\n");
            }
            await _next(context);
        }
    }
}
