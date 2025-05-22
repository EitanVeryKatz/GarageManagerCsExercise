using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryHours)
        {
            MaxEnergyCapacity = i_MaxBatteryHours;
            CurrentEnergyAmount = 0;
        }

        public void ChargeBattery(float i_HoursToAdd)
        {
            AddEnergy(i_HoursToAdd);
        }

        public override void AddEnergy(float i_AmountToAdd)
        {
            if (CurrentEnergyAmount + i_AmountToAdd > MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity - CurrentEnergyAmount);
            }

            CurrentEnergyAmount += i_AmountToAdd;
        }
        
    }
}

