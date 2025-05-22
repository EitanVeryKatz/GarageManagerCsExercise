using System;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public readonly string r_ModelName;
        public readonly string r_LicenseID;
        protected Wheel[] m_Wheels;
        private float m_energySource;

        public Vehicle(string i_ModelName, string i_LicenseID)
        {
            r_ModelName = i_ModelName;
            r_LicenseID = i_LicenseID;
        }

        

        

        protected class Wheel
        {
            public string m_ManufacturerName;
            public readonly float r_MaximumAllowedAirPressure;
            public float CurrentAirPressure { get; set; }

            public Wheel(float i_MaximumAllowedAirPressure)
            {
                r_MaximumAllowedAirPressure = i_MaximumAllowedAirPressure;
                CurrentAirPressure = 0;
            }

            public void AddAir(float i_AirToAdd)
            {
                if (CurrentAirPressure + i_AirToAdd <= r_MaximumAllowedAirPressure)
                {
                    CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(
                        0,
                        r_MaximumAllowedAirPressure - CurrentAirPressure,
                        "Cannot add air beyond the maximum allowed pressure.");
                }

            }

        }
        public void FillAirInTires() 
        {
            foreach (Wheel wheel in m_Wheels)
            {
                float missingAir = wheel.r_MaximumAllowedAirPressure - wheel.CurrentAirPressure;
                wheel.AddAir(missingAir);
            }

        }

    }

}
