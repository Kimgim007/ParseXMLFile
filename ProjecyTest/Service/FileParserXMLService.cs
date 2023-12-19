using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ProjecyTest.Entity;
using ProjecyTest.RabbitMQ;
using Serilog;
using Serilog.Core;

namespace ProjecyTest.Service
{
    public static class FileParserXMLService
    {
        // Этот метод для консольного интерфейса и использования многопоточности 
        public static void FileParserService()
        {
            bool answer = true;
            string[] xmlFiles = { };

            while (answer)
            {
                Log.Information("Укажите путь к папке где лежат XML файлы для последующей их обработки");
               
                string pathInFolder = pathInFolder = Console.ReadLine();
                Console.Clear();
                try
                {
                    xmlFiles = Directory.GetFiles(pathInFolder, "*.xml");
                    answer = false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при указании пути к папке");
                
                }
            }
            Console.Clear();    

            Log.Information("Файлы загружены успешно");
       
            Parallel.ForEach(xmlFiles, xmlFile =>
            {
                XmlDocument xmlDoc = new XmlDocument();
                XMLFileModelEntity xMLFileModel = new XMLFileModelEntity();

                string path = xmlFile;
                xmlDoc.Load(path);

                XmlNode xmlNode;

                if (xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlNode = xmlDoc.LastChild;
                }
                else
                {
                    xmlNode = xmlDoc.FirstChild;
                }

                if (xmlNode != null)
                {
                    try
                    {
                        Log.Information("Начинается обработка файла");
                       
                        FileParserService(xmlNode, xMLFileModel);

                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                       
                        Log.Error(ex, $"Произошла ошибка! Файл {xmlFile} не был обработан.");
                    }

                    try
                    {
                        RabbitMQSendMessage.SendMessage(JsonSerialiaerEntity.ClassToJsonString(xMLFileModel));
                    }
                    catch (Exception ex)
                    {
                     
                        Log.Error(ex, $"Произошла ошибка, файл {xmlFile} не был отправлен");
                    }
                }
            });
        }
        // Основаня логика для обработки XML файла
        public static void FileParserService(XmlNode xmlNode, XMLFileModelEntity xMLFileModel)
        {
            if (xmlNode.HasChildNodes)
            {
                if (xmlNode.NodeType == XmlNodeType.Element)
                {         
                    xMLFileModel.ElementTagName = xmlNode.Name;

                    if (xmlNode.Attributes.Count != 0)
                    {
                     
                        foreach (XmlAttribute attr in xmlNode.Attributes)
                        {
                            xMLFileModel.Attributes.Add(attr.Name, attr.Value);
                        }
                    }

                    if (xmlNode.HasChildNodes)
                    {
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            XMLFileModelEntity xMLFile = new XMLFileModelEntity();
                            if (xmlNode.ChildNodes.Count == 1 && xmlNode.InnerText[0] != '<')
                            {
                                if (xmlNode.Name == "ModuleState")
                                {
                                    string[] onlineRunNotReadyOffline = new[] { "Online", "Run", "NotReady", "Offline" };
                                    Random rnd = new Random();
                                    xMLFileModel.ElementValue = $"{onlineRunNotReadyOffline[rnd.Next(onlineRunNotReadyOffline.Length)]}";
                                }
                                else
                                {
                                    xMLFileModel.ElementValue = xmlNode.InnerText;
                                }
                            }

                            if (xmlNode.ChildNodes.Count == 1 && xmlNode.ChildNodes[0].NodeType == XmlNodeType.Text && xmlNode.InnerText[0] != '<')
                            {

                            }
                            else
                            {

                                xMLFileModel.Elements.Add(xMLFile);
                                FileParserService(xmlNode.ChildNodes[i], xMLFileModel.Elements[i]);
                            }
                        }
                    }
                }
            }
            if (xmlNode.NodeType == XmlNodeType.Text)
            {
                if (xmlNode.InnerText.Length > 0)
                {
                    // Декодирование HTML-специальных символов в тексте
                    string decodedRapidControlStatus = WebUtility.HtmlDecode(xmlNode.InnerText);

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(decodedRapidControlStatus);

                    if (xmlDocument.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        xmlNode = xmlDocument.LastChild;
                    }
                    else
                    {
                        xmlNode = xmlDocument.FirstChild;
                    }
                    FileParserService(xmlNode, xMLFileModel);
                }
            }
        }
    }
}
