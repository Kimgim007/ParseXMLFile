using System.Xml;
using System.Net;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;
using ProjecyTest.Service;
using Serilog;
using Serilog.Events;

namespace ProjecyTest
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            try
            {
                FileParserXMLService.FileParserService();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Произошла критическая ошибка");
            }
            finally
            {
                Log.CloseAndFlush();
            }      
        }
    }
}



