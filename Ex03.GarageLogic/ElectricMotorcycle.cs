using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryCapacityMinutes = 192;
        ElectricEngine r_Engine = new ElectricEngine(k_MaxBatteryCapacityMinutes);

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        internal override float EnergySourcePercentage
        {
            get
            {
                return (r_Engine.MinutesLeftInBattery / r_Engine.MaxMinutesOfUsage) * 100;
            }
            set
            {
                r_Engine.MinutesLeftInBattery = (value / 100) * r_Engine.MaxMinutesOfUsage;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Battery Percentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_FuelType = eFuelTypes.None)
        {
            if (i_FuelType != eFuelTypes.None)
            {
                throw new ArgumentException("Error: tried to add fuel to electric vehicle");
            }

            r_Engine.ChargeBattery(i_EnergySourceAmountToAdd);
        }

    }

}
