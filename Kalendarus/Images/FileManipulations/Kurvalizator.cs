using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarus.Images.FileManipulations
{
    internal class Kurvalizator
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static void mySer<T>(T obj, string file)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path + "\\" + file, json);
        }
        public static T myDeser<T>(string file)
        {
            if (File.Exists(path + "\\" + file))
            {
                string json = File.ReadAllText(path + "\\" + file);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            return default(T);
        }
    }
}
