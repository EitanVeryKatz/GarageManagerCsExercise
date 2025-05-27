namespace Ex03.GarageLogic
{
    internal class ElectricEngine
    {
        public float MaxMinutesOfUsage { get; set; }

        public float MinutesLeftInBattery { get; set; }

        public ElectricEngine(float i_maxBatteryMinutes)
        {
            MaxMinutesOfUsage = i_maxBatteryMinutes;
            MinutesLeftInBattery = 0;
        }

        public void ChargeBattery(float i_amountToAdd)
        {
            if (MinutesLeftInBattery + i_amountToAdd > MaxMinutesOfUsage)
            {
                throw new ValueOutOfRangeException(0, MaxMinutesOfUsage - MinutesLeftInBattery);
            }

            MinutesLeftInBattery += i_amountToAdd;
        }

    }

}

