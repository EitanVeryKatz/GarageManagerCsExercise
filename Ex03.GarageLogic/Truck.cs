//VehicleCreator.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private readonly FuelEngine m_engine = new FuelEngine(FuelEngine.e_FuelTypes.Soler, 135);
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;
        
        public Truck(string i_ModelName, string i_LicenseID)
            : base(i_ModelName, i_LicenseID)
        {
           
        }

        public float CurrentFuelAmount
        {
            get { return m_engine.CurrentFuelAmount; }
            set { m_engine.CurrentFuelAmount = value; }
        }

      

    }

}