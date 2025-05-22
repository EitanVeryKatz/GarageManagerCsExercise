using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine
    {
        public float MaxFuelCapacity { get; set; }
        public float CurrentFuelAmount {  get; set; }
        public enum e_FuelTypes { Octan98, Octan96, Octan95, Soler }
        public e_FuelTypes FuelType { get; }

        public FuelEngine(e_FuelTypes i_FuelType, float i_MaxFuelCapacity)
        {
            FuelType = i_FuelType;
            MaxFuelCapacity = i_MaxFuelCapacity;
            CurrentFuelAmount = 0;
        }

        public void Refuel(float i_AmountToAdd, e_FuelTypes i_FuelType)
        {
            if (i_FuelType != FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            AddFuel(i_AmountToAdd);
        }

        public void AddFuel(float i_AmountToAdd)
        {
            if (CurrentFuelAmount + i_AmountToAdd > MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, MaxFuelCapacity - CurrentFuelAmount);
            }

            CurrentFuelAmount += i_AmountToAdd;
        }
        
    }
}

