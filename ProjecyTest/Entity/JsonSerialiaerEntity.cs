using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjecyTest.Entity
{
    public static class JsonSerialiaerEntity
    {
        public static string messageString { get; set; }
        public static string ClassToJsonString(XMLFileModelEntity xMLFileModel)
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
