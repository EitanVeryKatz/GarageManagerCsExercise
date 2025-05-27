using System;

namespace Ex03.GarageLogic
{
    internal class FuelEngine
    {
        public float MaxFuelCapacity { get; set; }
        public float CurrentFuelAmount { get; set; }
        public  Vehicle.eFuelTypes FuelType { get; }

        public FuelEngine(Vehicle.eFuelTypes i_fuelType, float i_maxFuelCapacity)
        {
            FuelType = i_fuelType;
            MaxFuelCapacity = i_maxFuelCapacity;
            CurrentFuelAmount = 0;
        }

        public void Refuel(float i_amountToAdd, Vehicle.eFuelTypes i_fuelType)
        {
            if (i_fuelType != FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            AddFuel(i_amountToAdd);
        }

        public void AddFuel(float i_amountToAdd)
        {
            if (CurrentFuelAmount + i_amountToAdd > MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, MaxFuelCapacity - CurrentFuelAmount);
            }

            CurrentFuelAmount += i_amountToAdd;
        }

    }

}