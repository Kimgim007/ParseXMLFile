using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessorService.Service;
using DataProcessorService.Repository;
using DataProcessorService.MyDbContext;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DataProcessorService.Entitys.Entity;
namespace DataProcessorService.RabbitMQ
{
    public static class RabbitMQAcceptMassage
    {
        public static RabbitMQConfigEntity LoadConfig()
        {
            string jsonFileConfig = File.ReadAllText("E:\\Тестовое задание перезборка\\DataProcessorService\\appsettings.json");
            return JsonConvert.DeserializeObject<RabbitMQConfigEntity>(jsonFileConfig); ;

        }
        public static void AcceptMessage()
        {
            //E:\Не брак
            // Создание фабрики соединения
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

                // Создание подписчика
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    SQLiteDataBase _sQLiteDataBase = new SQLiteDataBase();
                    ModuleCatIDModuleStateRepository _moduleCatIDModuleStateRepository = new ModuleCatIDModuleStateRepository(_sQLiteDataBase);
                    ModuleCategoryIDModuleStateService moduleCategoryIDModuleStateService = new ModuleCategoryIDModuleStateService(_moduleCatIDModuleStateRepository);
                    moduleCategoryIDModuleStateService.StringProcessing(message);

                    // Обработка полученного сообщения (ваш код)
                    //Console.WriteLine("Received message: {0}", message);
                };

                // Начало прослушивания очереди
                channel.BasicConsume(queue: "my_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Ожидание сообщения");
                Console.ReadLine();
            }
        }
    }
}
