using System;

namespace Ex03.GarageLogic
{
    internal class FuelEngine
    {
        public float MaxFuelCapacity { get; set; }
        public float CurrentFuelAmount { get; set; }
        public  Vehicle.eFuelTypes FuelType { get; }

        public FuelEngine(Vehicle.eFuelTypes i_FuelType, float i_maxFuelCapacity)
        {
            FuelType = i_FuelType;
            MaxFuelCapacity = i_maxFuelCapacity;
            CurrentFuelAmount = 0;
        }

        public void Refuel(float i_AmountToAdd, Vehicle.eFuelTypes i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            addFuel(i_AmountToAdd);
        }

        private void addFuel(float i_AmountToAdd)
        {
            if (CurrentFuelAmount + i_AmountToAdd > MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, MaxFuelCapacity - CurrentFuelAmount);
            }

            CurrentFuelAmount += i_AmountToAdd;
        }

    }

}