using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle:Motorcycle, IElectric
    {
        ElectricEngine m_engine = new ElectricEngine(1);

        public ElectricMotorcycle(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {

        }

        void IElectric.Recharge(int i_minutesToCharge)
        {
            m_engine.ChargeBattery((float)i_minutesToCharge/60);
        }
    }
}
