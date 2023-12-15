﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjecyTest
{
    public static class FileParser
    {
        public static void FileParserService()
        {
            Console.WriteLine("Укажите путь к папке где лежат XML файлы для последующей их обработки");
            string pathInFolder = Console.ReadLine();
            //E:\Не брак
            
            string[] xmlFiles = Directory.GetFiles(pathInFolder, "*.xml");

            XmlDocument xmlDoc = new XmlDocument();

            XMLFileModel xMLFileModel = new XMLFileModel();

            foreach (string xmlFile in xmlFiles)
            {
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
                    FileParserService(xmlNode, xMLFileModel);

                    RabbitMQSendMessage.SendMessage(JsonSerialiaerClass.ClassToJsonString(xMLFileModel));

                    Thread.Sleep(1000);
                    Console.WriteLine();
                }
            }
        }
        public static void FileParserService(XmlNode xmlNode, XMLFileModel xMLFileModel)
        {
            if (xmlNode.HasChildNodes)
            {
                if (xmlNode.NodeType == XmlNodeType.Element)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Имя узла {xmlNode.Name}");

                    xMLFileModel.ElementTagName = xmlNode.Name;

                    if (xmlNode.Attributes.Count != 0)
                    {
                        Console.WriteLine("Атребуты узла:");
                        foreach (XmlAttribute attr in xmlNode.Attributes)
                        {
                            xMLFileModel.Attributes.Add(attr.Name, attr.Value);
                            Console.WriteLine($"Имя атребута: {attr.Name} - значение: {attr.Value}");
                        }
                    }

                    if (xmlNode.HasChildNodes)
                    {
                        for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                        {
                            XMLFileModel xMLFile = new XMLFileModel();
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
                                FileParserService(xmlNode.ChildNodes[i]);
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
                Console.WriteLine();
            }
        }
        public static void FileParserService(XmlNode xmlNode)
        {
            if (xmlNode.NodeType == XmlNodeType.Text)
            {
                if (xmlNode.InnerText.Length > 0)
                {
                    if (xmlNode.InnerText[0] != '<')
                    {
                        if (xmlNode.ParentNode.Name == "ModuleState")
                        {
                            string[] onlineRunNotReadyOffline = new[] { "Online", "Run", "NotReady", "Offline" };
                            Random rnd = new Random();
                            //Console.WriteLine($"Текстовое значение узла: {onlineRunNotReadyOffline[rnd.Next(onlineRunNotReadyOffline.Length)]}");
                            Console.WriteLine($"Текстовое значение узла:{xmlNode.InnerText}");
                        }
                        else
                        {
                            Console.WriteLine($"Текстовое значение узла:{xmlNode.InnerText}");
                        }

                    }
                    Console.WriteLine();
                }
            }
        }
    }
}