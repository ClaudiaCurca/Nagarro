using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;
using Microsoft.VisualBasic;

namespace iQuest.VendingMachine.FileFormat
{
    internal class XMLFile<T> : IFileCreator<T>
    {
        public void Write(List<T> products, string name)
        {
            string date = DateAndTime.Now.ToString("-yyyyMMdd_HHmmss");

            string directoryPath = ConfigurationManager.AppSettings["PathXML"];
            if (Directory.Exists(directoryPath))
            {
                string path = directoryPath + name + date + ".xml";
                XmlSerializer x = new XmlSerializer(products.GetType());
                using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings()
                    {
                        Indent = true,
                        IndentChars = "\t"
                    }))
                    {
                        x.Serialize(xmlWriter, products);
                    }
                }
            }
            else 
            { 
                throw new FileNotFoundException(); 
            }
        }

    }
}
