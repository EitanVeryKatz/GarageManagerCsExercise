using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private readonly FuelEngine m_Engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 5.8f);

        public FuelMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        void .Refuel(Vehicle.eFuelTypes i_fuelType, float i_fuelAmountToAdd)
        {
            m_Engine.Refuel(i_fuelAmountToAdd, i_fuelType);
        }
        Vehicle.eFuelTypes .GetFuelType()
        {
            return m_Engine.FuelType;
        }

        internal override float EnergySourcePercentage
        {
            get
            {

                return (m_Engine.CurrentFuelAmount / m_Engine.MaxFuelCapacity) * 100;
            }
            set
            {
                m_Engine.CurrentFuelAmount = (value / 100) * m_Engine.MaxFuelCapacity;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = m_Engine.FuelType.ToString();
            vehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

    }

}