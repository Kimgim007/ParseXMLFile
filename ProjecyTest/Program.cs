using System.Xml;
using System.Net;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;
using ProjecyTest.Service;

namespace ProjecyTest
{
    public class Program
    {
        public static void Main()
        {
            FileParserXMLService.FileParserService();          
        }
    }
}



