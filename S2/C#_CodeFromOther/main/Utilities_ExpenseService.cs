using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Security.Principal;
using ExpenseTracker.Models;

namespace ExpenseTracker.Utilities
{
    public static class ExpenseService
    {
        private static string root = "C:\\test1\\";
        static ExpenseService()
        {
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }
        public static void Create(Expense expense)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Expense));
            using (Stream stream = new FileStream($"{root}{expense.ExpenseId}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, expense);
            }
        }
        public static void Update(Expense expense)
        {
            Delete(expense.ExpenseId);
            Create(expense);
        }
        public static void Delete(string ID)
        {
            string filename = $"{root}{ID}.xml";
            if (File.Exists(filename)) File.Delete(filename);
            else throw new Exception($"ERROR: Tried to delete an account that does not exists ({ID}).");
                    
        }
        public static Expense Get(string ID)
        {
            string filename = $"{root}{ID}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Expense));

            if (File.Exists(filename))
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    Expense expense = (Expense)serializer.Deserialize(stream);

                    return expense;
                }
            }
            else
            {
                throw new Exception($"ERROR: Tried to load an account that does not exist ({ID}).");
            }
        }
        //public static List<Expense> GetAll()
        //{
        //    List<Expense> expenses = new List<Expense>();
        //    var file = Directory.GetFiles(root, "*.xml");
        //    foreach(var item in file)
        //    {
        //        using (var stream = new FileStream(item, FileMode.Open))
        //        {
        //            var serializer = new XmlSerializer(typeof(Expense));
        //            Expense oneExpense = (Expense)serializer.Deserialize(stream);
        //            expenses.Add(oneExpense);
        //        }
        //    }
        //    return expenses;
        //}
        public static Stack<Expense> GetAll()
        {
            Stack<Expense> expenses = new Stack<Expense>();
            var file = Directory.GetFiles(root, "*.xml");
            foreach (var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Expense));
                    Expense oneExpense = (Expense)serializer.Deserialize(stream);
                    expenses.Push(oneExpense);
                }
            }
            return expenses;
        }
    }
}
