using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using BMIcalculator.Models;
namespace BMIcalculator.Utilities
{
    public class BMIService
    {
        private static string root = "c:\\BMItest\\";
        static BMIService()
        {
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
        }
        public static void Create(BMI bmi)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BMI));
            using (Stream stream = new FileStream($"{root}{bmi.Index}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, bmi);
            }
        }
        public static void Delete(string index)
        {
            string filename = $"{root}{index}.xml";
            if (File.Exists(filename)) File.Delete(filename);
            else throw new Exception($"ERROR: Tried to delete an account that does not exists ({index}).");
        }
        public static BMI Get(string index)
        {
            string filename = $"{root}{index}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(BMI));

            if (File.Exists(filename))
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    BMI bmi = (BMI)serializer.Deserialize(stream);

                    return bmi;
                }
            }
            else
            {
                throw new Exception($"ERROR: Tried to load an account that does not exist ({index}).");
            }
        }
        public static List<BMI> Search(string name, List<BMI> list)
        {
            var passable = from n in list where n.Name == name select n;
            return passable.ToList();
        }
        public static List<BMI> Search(DateTime date, List<BMI> list)
        {
            var passable = from n in list where n.Date == date select n;
            return passable.ToList();
        }
        public static List<BMI> Search(BMIcalculator.Models.Type type, List<BMI> list)
        {
            var passable = from n in list where n.Type == type select n;
            return passable.ToList();
        }
        public static List<BMI> Search(double height, List<BMI> list, int a)
        {
            var passable = from n in list where n.Height == height select n;
            return passable.ToList();
        }
        public static List<BMI> Search(double weight, List<BMI> list, string a)
        {
            var passable = from n in list where n.Weight == weight select n;
            return passable.ToList();
        }
        public static List<BMI> GetAll()
        {
            List<BMI> bmis = new List<BMI>();
            var file = Directory.GetFiles(root, "*.xml");
            foreach (var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(BMI));
                    BMI onebmi = (BMI)serializer.Deserialize(stream);
                    bmis.Add(onebmi);
                }
            }
            return bmis;
        }
    }
}
