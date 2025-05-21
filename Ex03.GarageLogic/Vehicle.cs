using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public readonly string r_ModelName;
        public readonly string r_LicenseID;
        public float EnergySourcePrecentage {  get; set; }

        protected Wheel[] m_Wheels;

        protected class Wheel
        {
            public string m_ManufacturerName;
            public readonly float r_MaximumAllowedAirPressure;
           
            public float CurrentAirPressure {  get; set; }

            public Wheel(float i_MaximumAllowedAirPressure)
            {
                r_MaximumAllowedAirPressure = i_MaximumAllowedAirPressure;
            }

            public void AddAir(float i_airToAdd)
            {
                if(CurrentAirPressure + i_airToAdd <= r_MaximumAllowedAirPressure)
                {
                    CurrentAirPressure += i_airToAdd;
                }

            }
        }

        public Vehicle(string i_ModelName, string i_LicenseID) 
        {
            r_ModelName = i_ModelName;
            r_LicenseID = i_LicenseID;
        }

        
    }
}
