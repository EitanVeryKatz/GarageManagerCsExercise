using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryCapacityMinutes = 288;
        private readonly ElectricEngine r_Engine = new ElectricEngine(k_MaxBatteryCapacityMinutes);

        public ElectricCar(string i_licenseID, string i_modelName) : base(i_modelName, i_licenseID)
        {

        }

        void Recharge(float i_minutesToCharge)
        {
            r_Engine.ChargeBattery(i_minutesToCharge / 60);
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

        public override void AddToEnergySource(float i_energySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            if(i_fuelType != eFuelTypes.None)
            {
                throw new ArgumentException("Error: tried to add fuel to electric vehicle");
            }

            r_Engine.ChargeBattery(i_energySourceAmountToAdd);
        }

    }

}