using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VehicleManagementSystem.Models
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public Color Color { get; set; }
        public MakeType MakeType { get; set; }
        public Owner Owner { get; set; }
    }
    public enum Color
    {
        White,
        Black,
        Grey,
        Other
    }
    public enum MakeType
    {
        Lexus,
        Toyota,
        BWM,
        Other
    }
}
