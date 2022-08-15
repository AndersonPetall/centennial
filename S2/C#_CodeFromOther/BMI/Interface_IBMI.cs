
using BMICalc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalc.Interface
{
    public interface IBMI 
    {
        

        public void Create(BMI obj);
        public List<BMI> GetAll();
        public void Update(BMI obj);

        public void Delete(BMI obj);
    }
}
