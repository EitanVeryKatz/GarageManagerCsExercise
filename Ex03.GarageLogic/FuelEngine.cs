using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelEngine
    {
        internal float MaximumFuelCapacity { get; }
        internal float CurrentFuelAmount { get; set; }
        e_FuelTypes FuelType {  get;}
        public enum e_FuelTypes
        {
            Octan98,
            Octan96,
            Octan95,
            Soler
        }

        public FuelEngine(e_FuelTypes i_FuelType, float i_MaximumFuelCapacity)
        {
            MaximumFuelCapacity = i_MaximumFuelCapacity;
            FuelType = i_FuelType;
            CurrentFuelAmount = 0;
        }

        protected void addFuel(e_FuelTypes i_fuelType, float i_amountOfFuelToAdd)
        {
            if (i_fuelType == FuelType)
            {
                if (i_amountOfFuelToAdd + CurrentFuelAmount <= MaximumFuelCapacity)
                {
                    CurrentFuelAmount += i_amountOfFuelToAdd;
                }
            }
        }
    }

    
}
