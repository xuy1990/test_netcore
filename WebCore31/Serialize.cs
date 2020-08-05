using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text;

namespace WebCore31
{
    public class Serialize
    {
        public void Test()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using(Utf8JsonWriter writer = new Utf8JsonWriter(ms))
                {
                    writer.WriteStartObject();
                    writer.WriteString("name", "hh");
                    writer.WriteNumber("id", 1);
                    writer.WriteEndObject();
                    writer.Flush();
                }
                Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
