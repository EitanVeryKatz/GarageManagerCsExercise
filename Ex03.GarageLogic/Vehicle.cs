using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public readonly string r_ModelName;
        public readonly string r_LicenseID;
        protected Wheel[] m_Wheels;
        public List<string> uniqueDataMembers = new List<string>();

        public int TireCount
        {
            get
            {
               return m_Wheels.Length;
            }

        }

        public Vehicle(string i_ModelName, string i_LicenseID)
        {
            r_ModelName = i_ModelName;
            r_LicenseID = i_LicenseID;
        }

        internal void SetTireInfo(string[,] i_tireInfo)
        {
            for (int i = 0;i < TireCount;i++)
            {
                m_Wheels[i].m_ManufacturerName = i_tireInfo[i,0];
                m_Wheels[i].CurrentAirPressure = float.Parse(i_tireInfo[i, 1]);
            }
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

        public void Refuel(FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd)
        {
            if(this is IFuelPowered fuelPoweredVehicle)
            {
                fuelPoweredVehicle.Refuel(i_fuelType, i_fuelAmountToAdd);
            }
        }

        public void Recharge(float i_minutesToCharge)
        {
            if (this is IElectric fuelPoweredVehicle)
            {
                fuelPoweredVehicle.Recharge(i_minutesToCharge);
            }
        }

        internal virtual Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = new Dictionary<string, string>();
            VehicleData["Vehicle Id"] = r_LicenseID;
            VehicleData["Model Name"] = r_ModelName;
            for (int i = 0; i < m_Wheels.Count(); i++)
            {
                VehicleData[$"Wheel #{i} Manufacturer"] = m_Wheels[i].m_ManufacturerName;
                VehicleData[$"Wheel #{i} Air Pressure"] = m_Wheels[i].CurrentAirPressure.ToString();
            }
            return VehicleData;
        }

        protected abstract float EnergySourcePrecentage {  get; }

    }

}
