using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine
    {
        public float MaxMinutesOfUsage {get;set;}
        public float MinutesLeftInBattery { get;set;}
        public ElectricEngine(float i_MaxBatteryHours)
        {
            MaxMinutesOfUsage = i_MaxBatteryHours;
            MinutesLeftInBattery = 0;
        }

        
        public void ChargeBattery(float i_AmountToAdd)
        {
            if (MinutesLeftInBattery + i_AmountToAdd > MaxMinutesOfUsage)
            {
                throw new ValueOutOfRangeException(0, MaxMinutesOfUsage - MinutesLeftInBattery);
            }

            MinutesLeftInBattery += i_AmountToAdd;
        }
        
    }
}

