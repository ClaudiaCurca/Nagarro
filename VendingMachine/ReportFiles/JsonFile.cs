using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace iQuest.VendingMachine.FileFormat
{
    internal class JsonFile<T> : IFileCreator<T>
    {
        public void Write(List<T> products, string name)
        {
            string date = DateAndTime.Now.ToString("-yyyyMMdd_HHmmss");
            string directoryPath = ConfigurationManager.AppSettings["PathJson"];
            if (Directory.Exists(directoryPath))
            {
                string path = ConfigurationManager.AppSettings["PathJson"] + name + date + ".json";
                using (StreamWriter file = File.CreateText(path))
                {
                    using (JsonTextWriter writer = new JsonTextWriter(file))
                    {
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 4;
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, products);
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
