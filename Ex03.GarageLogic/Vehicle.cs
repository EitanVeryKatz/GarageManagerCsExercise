using System;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public readonly string r_ModelName;
        public readonly string r_LicenseID;
        protected Wheel[] m_Wheels;
        protected Engine m_Engine;

        public Vehicle(string i_ModelName, string i_LicenseID)
        {
            r_ModelName = i_ModelName;
            r_LicenseID = i_LicenseID;
        }

        public float EnergySourcePercentage
        {
            get
            {
                if (m_Engine != null && m_Engine.MaxEnergyCapacity > 0)
                {
                    return (m_Engine.CurrentEnergyAmount / m_Engine.MaxEnergyCapacity) * 100;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Engine Engine
        {
            get { return m_Engine; }
        }

        // מחלקה פנימית לגלגל
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
    }
}
