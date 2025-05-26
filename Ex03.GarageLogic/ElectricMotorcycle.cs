using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        ElectricEngine m_Engine = new ElectricEngine(1);

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        void Recharge(float i_minutesToCharge)
        {
            m_Engine.ChargeBattery(i_minutesToCharge / 60);
        }

        internal override float EnergySourcePercentage
        {
            get
            {
                return (m_Engine.MinutesLeftInBattery / m_Engine.MaxMinutesOfUsage) * 100;
            }
            set
            {
                m_Engine.MinutesLeftInBattery = (value / 100) * m_Engine.MaxMinutesOfUsage;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Battery Percentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            if (i_fuelType != eFuelTypes.None)
            {
                throw new ArgumentException("Error: tried to add fuel to electric vehicle");
            }
            m_Engine.ChargeBattery(i_EnergySourceAmountToAdd);
        }

    }

}
