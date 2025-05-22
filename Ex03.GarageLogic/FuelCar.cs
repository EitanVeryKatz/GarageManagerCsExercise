using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private readonly FuelEngine engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98, 48);
        public FuelCar(string i_ModelName, string i_LicenseID)
        : base(i_ModelName, i_LicenseID)
        {
            m_Engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98, 48);
        }

    }
}
