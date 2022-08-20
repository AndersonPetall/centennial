using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagementSystem.Models
{
    public class Manager : People
    {
        public int CarNumber { get => Cars.Count(); }
        public List<Car> Cars { get; set; }
        public Manager()
        {
            Cars = new List<Car>();
        }

    }
}
