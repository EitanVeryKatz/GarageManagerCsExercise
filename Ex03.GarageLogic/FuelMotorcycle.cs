using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle:Motorcycle
    {
        private readonly FuelEngine engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98, 5.8f);
        public FuelMotorcycle(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            
        }
        
    }
}
