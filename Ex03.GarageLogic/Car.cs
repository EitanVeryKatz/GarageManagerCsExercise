using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaximumWheelAirPressure = 32;

        public eCarColors Color { get; set; }
        public int NumberOfDoors { get; set; }

        public Car(string i_modelName, string i_licenseID) : base(i_modelName, i_licenseID)
        {
            base.m_wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_wheels[i] = new Wheel(k_MaximumWheelAirPressure);
            }

            m_uniqueDataMembers.Add("Car Color");
            m_uniqueDataMembers.Add("Amount of Doors");
        }

        public enum eCarColors
        {
            Yellow,
            Black,
            White,
            Silver
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Car Color"] = Color.ToString();
            vehicleData["Amount of Doors"] = NumberOfDoors.ToString();

            return vehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_filledUniqueData)
        {
            Color = (eCarColors)Enum.Parse(typeof(eCarColors), i_filledUniqueData["Car Color"]);
            NumberOfDoors = int.Parse(i_filledUniqueData["Amount of Doors"]);
        }

    }

}
