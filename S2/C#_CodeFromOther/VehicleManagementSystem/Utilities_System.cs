using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementSystem.Models;
namespace VehicleManagementSystem.Utilities
{
    public interface System
    {
        void Create(Car car);
        void Create(Owner owner);
        void Create(Manager manager);
        void Update(Car car);
        void Update(Owner owner);
        void Update(Manager manager);
        void Delete(Car car);
        void Delete(Owner owner);
        void Delete(Manager manager);
        List<Car> GetAll(int a);
        List<Owner> GetAll(int a, int b);
        List<Manager> GetAll(int a, int b, int c);
    }
}
