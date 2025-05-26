using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car, IElectric
    {
        private const float k_MaxBatteryCapacityMinutes = 4000000;

        private ElectricEngine m_Engine = new ElectricEngine(k_MaxBatteryCapacityMinutes);

        public ElectricCar(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {
        }

        void IElectric.Recharge(float i_minutesToCharge)
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

    }

}