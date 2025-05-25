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
        public int AmountsOfDoors { get; set; }

        public Car(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];//initialize the wheels array
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);//create the wheels
            }

            uniqueDataMembers.Add("Car Color");
            uniqueDataMembers.Add("Amount of Doors");

        }

        public enum e_CarColors
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
            vehicleData["Amount of Doors"] = AmountsOfDoors.ToString();

            return vehicleData;
        }

        

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            Color = (e_CarColors)Enum.Parse(typeof(e_CarColors),i_FilledUniqueData["Car Color"]);
            AmountsOfDoors = int.Parse(i_FilledUniqueData["Amount of Doors"]);

        }

       
    }

}
