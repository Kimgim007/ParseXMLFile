using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using ProjecyTest.Entity;
using Serilog;

namespace ProjecyTest.RabbitMQ
{
    public static class RabbitMQSendMessage
    {
        public static RabbitMQConfigEntity LoadConfig()
        {
            // Местонахождение файла конфигурации
            //Пример соединения:
            //  string jsonFileConfig = File.ReadAllText("E:\\Тестовое задание \\ProjecyTest\\appsettings.json");
            string jsonFileConfig = File.ReadAllText("YouPath\\ProjecyTest\\appsettings.json");
            return JsonConvert.DeserializeObject<RabbitMQConfigEntity>(jsonFileConfig); ;
      
        }
        public static void SendMessage(string messeg)
        {
            RabbitMQConfigEntity config = LoadConfig();
            var factory = new ConnectionFactory
            {
                HostName = config.HostName,
                Port = config.Port,
                UserName = config.UserName,
                Password = config.Password
            };
            // Создание соединения
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Объявление очереди
                channel.QueueDeclare(queue: "my_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Конвертация JSON в байты
                var body = Encoding.UTF8.GetBytes(messeg);

                // Отправка сообщения в очередь
                channel.BasicPublish(exchange: "",
                                                 routingKey: "my_queue",
                                                 basicProperties: null,
                                                 body: body);
                Log.Information("Файл был отправлен через RabbitMQ");
             
            }
        }
    }
}
