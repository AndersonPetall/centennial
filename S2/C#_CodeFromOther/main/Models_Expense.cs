using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Record
    {
        public string Name { get; set; }
        public Responsors Responsor { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
        public enum Responsors
        {
            family,
            individual
        }
    }
}
