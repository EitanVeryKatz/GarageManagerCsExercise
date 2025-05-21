using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car, IFuelUsing
    {
        
        public FuelCar(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
        }

        void IFuelUsing.fillFuel(float i_fuelAmount, e_FuelTypes i_fuelType)
        {
            
        }

        float IFuelUsing.MaxFuelAmount
        {
            get
            {
                return 48;
            }

        }

        e_FuelTypes IFuelUsing.FuelType
        {
            get
            {
                return e_FuelTypes.Octan98;
            }
        }

        float IFuelUsing.CurrentFuelAmount
            

    }
}
