using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected class Wheel
        {
            public string m_manufacturerName;
            public readonly float r_maximumAllowedAirPressure;
            public float CurrentAirPressure { get; set; }

            public Wheel(float i_MaximumAllowedAirPressure)
            {
                r_maximumAllowedAirPressure = i_MaximumAllowedAirPressure;
                CurrentAirPressure = 0;
            }

            public void AddAir(float i_AirToAdd)
            {
                if (CurrentAirPressure + i_AirToAdd <= r_maximumAllowedAirPressure)
                {
                    CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(
                        0,
                        r_maximumAllowedAirPressure - CurrentAirPressure,
                        "Cannot add air beyond the maximum allowed pressure.");
                }

            }

        }

        public enum eFuelTypes { Octan98, Octan96, Octan95, Soler, None }

        public readonly string r_ModelName;
        public readonly string r_LicenseID;
        protected Wheel[] m_wheels;
        internal List<string> m_uniqueDataMembers = new List<string>();
        internal abstract float EnergySourcePercentage { get; set; }

        public int TireCount
        {
            get
            {
                return m_wheels.Length;
            }

        }

        public Vehicle(string i_modelName, string i_licenseID)
        {
            r_ModelName = i_modelName;
            r_LicenseID = i_licenseID;

        }

        internal void SetTireInfo(string[,] i_tireInfo)
        {
            for (int i = 0; i < TireCount; i++)
            {
                m_wheels[i].m_manufacturerName = i_tireInfo[i, 0];
                try
                {
                    m_wheels[i].CurrentAirPressure = float.Parse(i_tireInfo[i, 1]);
                }
                catch (FormatException)
                {
                    throw new FormatException("Invalid air pressure format. Please enter a valid number.");
                }

            }

        }

        public void FillAirInTires()
        {
            foreach (Wheel wheel in m_wheels)
            {
                float missingAir = wheel.r_maximumAllowedAirPressure - wheel.CurrentAirPressure;
                wheel.AddAir(missingAir);
            }

        }

        internal virtual Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = new Dictionary<string, string>();

            VehicleData["Vehicle Id"] = r_LicenseID;
            VehicleData["Model Name"] = r_ModelName;
            for (int i = 0; i < m_wheels.Count(); i++)
            {
                VehicleData[$"Wheel #{i} Manufacturer"] = m_wheels[i].m_manufacturerName;
                VehicleData[$"Wheel #{i} Air Pressure"] = m_wheels[i].CurrentAirPressure.ToString();
            }

            return VehicleData;
        }

        internal virtual eFuelTypes GetFuelType()
        {
            return eFuelTypes.None;
        }

        public abstract void AddToEnergySource(float i_fuelAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None);

        internal abstract void SetUniqueMembers(Dictionary<string, string> i_filledUniqueData);

    }

}
