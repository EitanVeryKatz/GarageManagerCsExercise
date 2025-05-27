using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryCapacityMinutes = 192;
        ElectricEngine r_engine = new ElectricEngine(k_MaxBatteryCapacityMinutes);

        public ElectricMotorcycle(string i_licenseID, string i_modelName) : base(i_modelName, i_licenseID)
        {

        }

        private void Recharge(float i_minutesToCharge)
        {
            r_engine.ChargeBattery(i_minutesToCharge / 60);
        }

        internal override float EnergySourcePercentage
        {
            get
            {
                return (r_engine.MinutesLeftInBattery / r_engine.MaxMinutesOfUsage) * 100;
            }
            set
            {
                r_engine.MinutesLeftInBattery = (value / 100) * r_engine.MaxMinutesOfUsage;
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

            r_engine.ChargeBattery(i_EnergySourceAmountToAdd);
        }

    }

}
