using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public string ExpenseId { get; private set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Record Record { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public enum Categories
        {
            grocery,
            accommodation,
            diet,
            transportation,
            necessities,
            other
        }
        public Expense()
        {
            this.ExpenseId = Guid.NewGuid().ToString();
        }
    }
}
