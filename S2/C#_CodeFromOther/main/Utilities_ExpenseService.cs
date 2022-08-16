using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Models;
using System.IO;
using System.Xml.Serialization;
namespace ExpenseTracker.Utilities
{
    public static class ExpenseService
    {
        private static string root = "";
        static ExpenseService()
        {
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }
        public static void Create(Expense expense)
        {

        }
        public static void Update(Expense expense)
        {

        }
        public static void Delete(Expense expense)
        {

        }
        public static List<Expense> GetAll()
        {
            List<Expense> expenses = new List<Expense>();
            var file = Directory.GetFiles(root, "*.xml");
            foreach(var item in file)
            {
                using (var stream = new FileStream(item, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Expense));
                    Expense oneExpense = (Expense)serializer.Deserialize(stream);
                    expenses.Add(oneExpense);
                }
            }
            return expenses;
        }
    }
}
