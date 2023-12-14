using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Program
{
    static void Main()
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
            channel.QueueDeclare(queue: "your_queue_name",
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

                // Обработка полученного сообщения (ваш код)
                Console.WriteLine("Received message: {0}", message);
            };

            // Начало прослушивания очереди
            channel.BasicConsume(queue: "your_queue_name",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Waiting for messages. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}




