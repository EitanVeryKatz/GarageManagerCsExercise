using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine
    {
        public float MaxHoursOfUsage {get;set;}
        public float CurrentAmountOfHoursLeftInBattery { get;set;}
        public ElectricEngine(float i_MaxBatteryHours)
        {
            MaxHoursOfUsage = i_MaxBatteryHours;
            CurrentAmountOfHoursLeftInBattery = 0;
        }

        
        public void ChargeBattery(float i_AmountToAdd)
        {
            if (CurrentAmountOfHoursLeftInBattery + i_AmountToAdd > MaxHoursOfUsage)
            {
                throw new ValueOutOfRangeException(0, MaxHoursOfUsage - CurrentAmountOfHoursLeftInBattery);
            }

            CurrentAmountOfHoursLeftInBattery += i_AmountToAdd;
        }
        
    }
}

