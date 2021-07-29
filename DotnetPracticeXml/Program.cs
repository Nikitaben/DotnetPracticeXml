using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Xml.Serialization;

namespace DotnetPracticeXml
{
    public class Item
    {
        public string id { get; set; }
        public string label { get; set; }
    }
    public class Menu
    {
        public string header { get; set; }

        public List<Item> items { get; set; }
    }
    public class Top
    {
        public Menu menu { get; set; }
        public void Print()
        {
            Console.WriteLine(menu.header);
            foreach (var i in menu.items)
            {
                if (i != null)
                {
                    if (i.id != null)
                        Console.WriteLine(i.id);
                    if (i.id != null)
                        Console.WriteLine(i.label);
                }
                else
                {
                    Console.WriteLine("null");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //string json = Console.ReadLine();
            string file = @"data.json";
            string json = File.ReadAllText(file);


            //Console.WriteLine(json);
            Top obj = JsonSerializer.Deserialize<Top>(json);
            obj.Print();
            /*var xs = new XmlSerializer(typeof(Top));
            string xmlfile = @"data.xml";

            using (FileStream fs = File.Create(xmlfile))
            {
                xs.Serialize(fs, obj);
            }*/

            ToXmlFile("dataFromMethod.xml", obj);
            Console.WriteLine("==============================");
            var resObj = FromXmlFile<Top>("dataFromMethod.xml");
            resObj.Print();
        }

        public static T FromXmlFile<T>(string file)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Top));
            var xmlContent = File.ReadAllText(file);
            using (StringReader sr = new StringReader(xmlContent))
            {
              
                return (T)xmls.Deserialize(sr);
            }
        }
        public static void ToXmlFile<T>(string file, T obj)
         { 
            using (StringWriter sw = new StringWriter(new StringBuilder())) 
            {
             XmlSerializer xmls = new XmlSerializer(typeof(Top));
             xmls.Serialize(sw, obj);
                File.WriteAllText(file, sw.ToString());



            }
         }
    }
}
