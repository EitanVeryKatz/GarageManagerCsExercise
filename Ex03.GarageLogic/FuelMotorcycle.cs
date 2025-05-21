using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle:Motorcycle, IFuelUsing
    {
        public FuelMotorcycle(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
        }
    }
}
