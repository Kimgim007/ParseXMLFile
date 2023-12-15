using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjecyTest
{
    public static class JsonSerialiaerClass
    {
        public static string messageString {  get; set; }
        public static string ClassToJsonString(XMLFileModel xMLFileModel)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            messageString = JsonSerializer.Serialize(xMLFileModel, options);

            return messageString;
        }
    }
}
