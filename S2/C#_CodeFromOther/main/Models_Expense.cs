using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public string ExpenseId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Record Record { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public Expense()
        {
            this.ExpenseId = Guid.NewGuid().ToString();
        }
        public Expense(int amount,DateTime date, Record record, string description):this()
        {
            this.Amount = amount;
            this.Date = date;
            this.Record = record;
            this.Description = description;
        }
        public enum Categories
        {
            grocery,
            accommodation,
            diet,
            transportation,
            necessities,
            other
        }
    }
}
