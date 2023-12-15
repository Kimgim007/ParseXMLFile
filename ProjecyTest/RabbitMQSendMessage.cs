using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjecyTest
{
    public static class RabbitMQSendMessage
    {
        public static void SendMessage(string messeg)
        {
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

                // Конвертация JSON в байты
                var body = Encoding.UTF8.GetBytes(messeg);

                // Отправка сообщения в очередь
                channel.BasicPublish(exchange: "",
                                                 routingKey: "my_queue",
                                                 basicProperties: null,
                                                 body: body);

                Console.WriteLine("Sent message to RabbitMQ");
            }

        }
    }

    
}
