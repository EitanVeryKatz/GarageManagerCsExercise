using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car:Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaximunWheelAirPressure = 32;

        public e_CarColors Color { get; set; }
        public e_possibleAmountsOfDoors AmountsOfDoors { get; set; }

        public Car(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];//initialize the wheels array
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);//create the wheels
            }

            uniqueDataMembers.Add("Color");
            uniqueDataMembers.Add("Amount of Doors");

        }

        public enum e_CarColors
        {
            yellow,
            black,
            white,
            silver
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Car Color"] = Color.ToString();
            vehicleData["Amount of Doors"] = AmountsOfDoors.ToString();

            return vehicleData;
        }

        public enum e_possibleAmountsOfDoors
        {
            two = 2,
            three = 3,
            four = 4,
            five = 5,
        }

    }

}
