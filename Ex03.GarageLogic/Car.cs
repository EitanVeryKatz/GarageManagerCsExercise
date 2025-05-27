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

        public Car(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximumWheelAirPressure);
            }

            m_UniqueDataMembers.Add("Car Color");
            m_UniqueDataMembers.Add("Amount of Doors");
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

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            Color = (eCarColors)Enum.Parse(typeof(eCarColors), i_FilledUniqueData["Car Color"]);
            NumberOfDoors = int.Parse(i_FilledUniqueData["Amount of Doors"]);
        }

    }

}
