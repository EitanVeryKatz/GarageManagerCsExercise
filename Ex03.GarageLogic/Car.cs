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
            five = 5
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            Color = toColor(i_FilledUniqueData["Car Color"]);
            AmountsOfDoors = toDoors(i_FilledUniqueData["Amount of Doors"]);

        }

        private e_CarColors toColor(string i_colorStr)
        {
            e_CarColors color = e_CarColors.black;
            switch(i_colorStr)
            {
                case ("black"):
                    color = e_CarColors.black;
                    break;
                case ("white"):
                    color = e_CarColors.white;
                    break;
                case ("silver"):
                    color = e_CarColors.silver;
                    break;
                case ("yellow"):
                    color = e_CarColors.yellow;
                    break;

            }
            return color;

        }//this is shit will be fixed 

        private e_possibleAmountsOfDoors toDoors(string i_doorsStr)
        {
            e_possibleAmountsOfDoors doors = e_possibleAmountsOfDoors.two;
            switch (i_doorsStr)
            {
                case ("2"):
                    doors = e_possibleAmountsOfDoors.two;
                    break;
                case ("3"):
                    doors = e_possibleAmountsOfDoors.three;
                    break;
                 case ("4"):
                    doors = e_possibleAmountsOfDoors.four;
                    break;
                case ("5"):
                    doors = e_possibleAmountsOfDoors.five;
                    break;
            }
            return doors;
        }
    }

}
