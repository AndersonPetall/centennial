
using BMICalc.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BMICalc.Model
{
    public class BMI : IBMI
    {
        
        public string root = @"C:\source\logs\";
        public string Name { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public double Feet { get; set; }
        public double Inches { get; set; }
        public double Meters { get; set; }
        public double Pounds { get; set; }
        public double Kilograms { get; set; }
        public double BMIndex { get; set; }
        public UnitType Units { get; set; }

        public BMI()
        {
            
            if (!Directory.Exists(this.root))
            {
                Directory.CreateDirectory(this.root);
            }
        }

        public BMI(string fname, string lname, UnitType unit, double feet = 0, double inches = 0, double meters = 0, double pounds = 0, double kg = 0)
        {
            Name = new Random().Next(10000, 99999).ToString();
            First = fname;
            Last = lname;
            Units = unit; 
            Feet = feet;
            Inches = inches;
            Meters = meters;
            Pounds = pounds;
            Kilograms = kg;
  
        }

        public override string ToString()
        {
            return $"{First} {Last} has a BMI of {BMIndex}";
        }

        public  double CalculateBMI(string firstName,string lastName, UnitType unit, double feet = 0, double inches = 0, double meters = 0, double pounds = 0, double kg = 0)
        {
            BMI newBMI = new BMI(firstName, lastName, unit, feet, inches, meters, pounds, kg);
            
            if (unit == UnitType.Metric)
            {
                var height = meters / 100;
                newBMI.BMIndex = Math.Round(kg/Math.Pow(height, 2), 2);
                Create(newBMI);
                return newBMI.BMIndex;
            }
            else
            {
                var height = (feet * 12) + inches;
                newBMI.BMIndex = Math.Round( pounds / Math.Pow(height, 2), 2)*703; 
                Create(newBMI);
                return newBMI.BMIndex;
            }
        }

        public void Create(BMI index)
        {
            string filename = $"{index.Name}.xml";
           
            
            if (File.Exists(filename))
            {
                throw new Exception("This file exists already.");

            }
            try
            {
                using (var stream = new FileStream(this.root + filename, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(BMI));
                    serializer.Serialize(stream, index);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error occured when trying to create file. " +
                    " {e.Message}");
            }
        }

        public List<BMI> GetAll()
        {

            List<BMI> indexes = new List<BMI>();
            var files = Directory.GetFiles(root, "*.xml");

            foreach (string file in files)
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(BMI));
                    BMI index = (BMI)serializer.Deserialize(stream);
                    indexes.Add(index);
                }
            }

            return indexes;
        }

        public void Update(BMI obj)
        {
            this.Delete(obj);
            this.Create(obj);


        }

        public void Delete(BMI obj)
        {
            string filename = $"{obj.Name}.xml";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }

    public enum UnitType{
            Metric,
            Imperial

        }

}
