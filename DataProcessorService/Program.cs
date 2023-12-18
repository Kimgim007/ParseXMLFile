using System.Data.SQLite;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Microsoft.Data.Sqlite;
using DataProcessorService.RabbitMQ;

class Program
{
    static void Main()
    {
        try
        {
            RabbitMQAcceptMassage.AcceptMessage();
        }
        catch
        {
            Console.WriteLine("Сообщение не было доставлено");
        }
       
    }
}




