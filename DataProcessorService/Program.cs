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
        //string databasePath = "DataBase.db";
        //string absolutePath = Path.GetFullPath(databasePath);
        //// Создание базы данных, если её нет
        //if (!System.IO.File.Exists(absolutePath))
        //{
        //    SQLiteConnection.CreateFile(absolutePath);
        //    Console.WriteLine($"База данных создана по пути: {absolutePath}");
        //}
        //else
        //{
        //    Console.WriteLine($"База данных уже существует по пути: {absolutePath}");
        //}

        RabbitMQAcceptMassage.AcceptMessage();



    }
}




