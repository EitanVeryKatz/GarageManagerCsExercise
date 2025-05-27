using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private readonly FuelEngine r_engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 48);

        public FuelCar(string i_licenseID, string i_modelName) : base(i_modelName, i_licenseID)
        {

        }

        public void Refuel(float i_amountToAdd, Vehicle.eFuelTypes i_fuelType)
        {
            if (i_fuelType != r_engine.FuelType)
            {
                throw new ArgumentException("Fuel type does not match the car's required fuel type.");
            }

            if (i_amountToAdd <= 0)
            {
                throw new ArgumentException("Amount to add must be positive.");
            }

            if (r_engine.CurrentFuelAmount + i_amountToAdd > r_engine.MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, r_engine.MaxFuelCapacity - r_engine.CurrentFuelAmount, "Fuel amount exceeds tank capacity.");
            }

            r_engine.Refuel(i_amountToAdd, i_fuelType);
            r_engine.CurrentFuelAmount += i_amountToAdd;
        }

        public Vehicle.eFuelTypes FuelType
        {
            get { return r_engine.FuelType; }
        }

        public float CurrentFuelAmount
        {
            get { return r_engine.CurrentFuelAmount; }
        }

        public float MaxFuelCapacity
        {
            get { return r_engine.MaxFuelCapacity; }
        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_engine.FuelType;
        }

        internal override float EnergySourcePercentage
        {
            get
            {
                return (r_engine.CurrentFuelAmount / r_engine.MaxFuelCapacity) * 100;
            }
            set
            {
                r_engine.CurrentFuelAmount = (value / 100) * r_engine.MaxFuelCapacity;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = r_engine.FuelType.ToString();
            vehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_energySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            r_engine.Refuel(i_energySourceAmountToAdd, i_fuelType);
        }

    }

}
