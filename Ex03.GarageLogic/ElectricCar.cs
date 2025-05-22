
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private readonly float r_MaxBatteryHours = 3.2f;
        private float m_CurrentBatteryHours;

        public ElectricCar(string i_ModelName, string i_LicenseID)
            : base(i_ModelName, i_LicenseID)
        {
            m_Engine = new ElectricEngine(r_MaxBatteryHours);
            m_CurrentBatteryHours = 0f;
        }

        public void Recharge(float i_HoursToAdd)
        {
            if (i_HoursToAdd <= 0)
            {
                throw new ArgumentException("Hours to add must be positive.");
            }

            if (m_CurrentBatteryHours + i_HoursToAdd > r_MaxBatteryHours)
            {
                throw new ValueOutOfRangeException(0, r_MaxBatteryHours - m_CurrentBatteryHours, "Charge amount exceeds battery capacity.");
            }

            m_Engine.AddEnergy(i_HoursToAdd);
            m_CurrentBatteryHours += i_HoursToAdd;
        }

        public float CurrentBatteryHours
        {
            get { return m_CurrentBatteryHours; }
        }

        public float MaxBatteryHours
        {
            get { return r_MaxBatteryHours; }
        }
    }
}