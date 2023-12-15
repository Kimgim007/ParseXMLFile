using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessorService.Service;

namespace DataProcessorService.RabbitMQ
{
    public static class RabbitMQAcceptMassage
    {     
        public static void AcceptMessage()
        {
            // Создание фабрики соединения
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // Имя или IP-адрес брокера
                Port = 5672,             // Порт брокера по умолчанию
                UserName = "guest",      // Имя пользователя брокера
                Password = "guest"       // Пароль пользователя брокера
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

                // Создание подписчика
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    ModuleCategoryIDModuleStateService moduleCategoryIDModuleStateService = new ModuleCategoryIDModuleStateService();
                    moduleCategoryIDModuleStateService.StringProcessing(message);

                    // Обработка полученного сообщения (ваш код)
                    //Console.WriteLine("Received message: {0}", message);
                };

                // Начало прослушивания очереди
                channel.BasicConsume(queue: "my_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Waiting for messages. Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
