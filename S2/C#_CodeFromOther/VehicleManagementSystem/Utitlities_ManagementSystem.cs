using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Models;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Windows;

namespace VehicleManagementSystem.Utilities
{
    internal class ManagementSystem : System
    {
        private static string root1 = "C:\\test1\\CAR\\";
        private static string root2 = "C:\\test1\\OWNER\\";
        private static string root3 = "C:\\test1\\MANAGER\\";
        static ManagementSystem()
        {
            if (!Directory.Exists(root1) || !Directory.Exists(root2) || !Directory.Exists(root3))
            {
                Directory.CreateDirectory(root1);
                Directory.CreateDirectory(root2);
                Directory.CreateDirectory(root3);
            }
        }
        #region interface 
        public void Create(Car car)
        {
            CreateCar(car);
        }
        public void Create(Owner owner)
        {
            CreateOwner(owner);
        }
        public void Create(Manager manager)
        {
            CreateManager(manager);
        }
        public void Update(Car car)
        {
            UpdateCar(car);
        }
        public void Update(Owner owner)
        {
            UpdateOwner(owner);
        }
        public void Update(Manager manager)
        {
            UpdateManager(manager);
        }
        public void Delete(Car car)
        {
            DeleteCar(car);
        }
        public void Delete(Owner owner)
        {
            DeleteOwner(owner);
        }
        public void Delete(Manager manager)
        {
            DeleteManager(manager);
        }
        public List<Car> GetAll(int a)
        {
            List<Car> cars = GetAllCar();
            return cars;
        }
        public List<Owner> GetAll(int a, int b)
        {
            List<Owner> owners = GetAllOwner();
            return owners;
        }
        public List<Manager> GetAll(int a, int b, int c)
        {
            List<Manager> managers = GetAllManager();
            return managers;
        }
        #endregion

        #region car
        public static void CreateCar(Car car)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Car));
            using (Stream stream = new FileStream($"{root1}{car.LicensePlate}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, car);
            }
        }
        public static void UpdateCar(Car car)
        {
            DeleteCar(car);
            CreateCar(car);
        }
        public static void DeleteCar(Car car)
        {
            string filename = $"{root1}{car.LicensePlate}.xml";
            if (File.Exists(filename)) File.Delete(filename);
            else throw new Exception($"ERROR: Tried to delete an account that does not exists ({car.LicensePlate}).");
        }
        public static List<Car> GetAllCar()
        {
            List<Car> cars = new List<Car>();
            var file = Directory.GetFiles(root1, "*.xml");
            foreach (var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Car));
                    Car onecar = (Car)serializer.Deserialize(stream);
                    cars.Add(onecar);
                }
            }
            return cars;
        }
        public static List<Car> SearchCar(string index, List<Car> list)
        {
            var passable = from n in list where n.LicensePlate == index select n;
            return passable.ToList();
        }
        #endregion

        #region owner
        public static void CreateOwner(Owner owner)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Owner));
            using (Stream stream = new FileStream($"{root2}{owner.Name}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, owner);
            }
        }
        public static void UpdateOwner(Owner owner)
        {
            DeleteOwner(owner);
            CreateOwner(owner);
        }
        public static void DeleteOwner(Owner owner)
        {
            string filename = $"{root2}{owner.Name}.xml";
            if (File.Exists(filename)) File.Delete(filename);
            else throw new Exception($"ERROR: Tried to delete an account that does not exists ({owner.Name}).");
        }
        public static List<Owner> GetAllOwner()
        {
            List<Owner> owners = new List<Owner>();
            var file = Directory.GetFiles(root2, "*.xml");
            foreach (var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Owner));
                    Owner oneowner = (Owner)serializer.Deserialize(stream);
                    owners.Add(oneowner);
                }
            }
            return owners;
        }
        public static List<Owner> SearchOwner(string name, List<Owner> list)
        {
            var passable = from n in list where n.Name == name select n;
            return passable.ToList();
        }
        #endregion

        #region manager
        public static void CreateManager(Manager manager)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Manager));
            using (Stream stream = new FileStream($"{root3}{manager.Name}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, manager);
            }
        }
        public static void UpdateManager(Manager manager)
        {
            DeleteManager(manager);
            CreateManager(manager);
        }
        public static void DeleteManager(Manager manager)
        {
            string filename = $"{root3}{manager.Name}.xml";
            if (File.Exists(filename)) File.Delete(filename);
            else throw new Exception($"ERROR: Tried to delete an account that does not exists ({manager.Name}).");
        }
        public static List<Manager> GetAllManager()
        {
            List<Manager> managers = new List<Manager>();
            var file = Directory.GetFiles(root3, "*.xml");
            foreach (var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Manager));
                    Manager onemanager = (Manager)serializer.Deserialize(stream);
                    managers.Add(onemanager);
                }
            }
            return managers;
        }
        public static List<Manager> SearchManager(string name, List<Manager> list)
        {
            var passable = from n in list where n.Name == name select n;
            return passable.ToList();
        }
        #endregion
    }
}
