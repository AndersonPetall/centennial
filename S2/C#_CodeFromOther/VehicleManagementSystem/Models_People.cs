using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagementSystem.Models
{
    public class People
    {
        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; } 
        }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - Birthday.Year;
                age = DateTime.Now.DayOfYear > Birthday.DayOfYear ? age : --age;
                return age;
            }
        }

    }
}
