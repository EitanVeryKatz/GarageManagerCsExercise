using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private readonly FuelEngine r_Engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 48);

        public FuelCar(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        public void Refuel(float i_AmountToAdd, Vehicle.eFuelTypes i_FuelType)
        {
            if (i_FuelType != r_Engine.FuelType)
            {
                throw new ArgumentException("Fuel type does not match the car's required fuel type.");
            }

            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("Amount to add must be positive.");
            }

            if (r_Engine.CurrentFuelAmount + i_AmountToAdd > r_Engine.MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, r_Engine.MaxFuelCapacity - r_Engine.CurrentFuelAmount, "Fuel amount exceeds tank capacity.");
            }

            r_Engine.Refuel(i_AmountToAdd, i_FuelType);
            r_Engine.CurrentFuelAmount += i_AmountToAdd;
        }

        public Vehicle.eFuelTypes FuelType
        {
            get { return r_Engine.FuelType; }
        }

        public float CurrentFuelAmount
        {
            get { return r_Engine.CurrentFuelAmount; }
        }

        public float MaxFuelCapacity
        {
            get { return r_Engine.MaxFuelCapacity; }
        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_Engine.FuelType;
        }

        internal override float EnergySourcePercentage
        {
            get
            {
                return (r_Engine.CurrentFuelAmount / r_Engine.MaxFuelCapacity) * 100;
            }
            set
            {
                r_Engine.CurrentFuelAmount = (value / 100) * r_Engine.MaxFuelCapacity;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = r_Engine.FuelType.ToString();
            vehicleData["Fuel Tank Percentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_FuelType = eFuelTypes.None)
        {
            r_Engine.Refuel(i_EnergySourceAmountToAdd, i_FuelType);
        }

    }

}
