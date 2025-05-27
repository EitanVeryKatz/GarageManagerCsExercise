using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseTypes
        {
            A,
            A2,
            AB,
            B2
        }

        private const int k_NumOfWheels = 2;
        private const float k_MaximunWheelAirPressure = 30;

        public eMotorcycleLicenseTypes License { get; set; }

        public int EngineVolume { get; set; }

        public Motorcycle(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

            UniqueDataMembers.Add("License Type");
            UniqueDataMembers.Add("Engine Volume");
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["License Type"] = License.ToString();
            vehicleData["Engine Volume"] = EngineVolume.ToString() + " cc";

            return vehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            License = (eMotorcycleLicenseTypes)Enum.Parse(typeof(eMotorcycleLicenseTypes), i_FilledUniqueData["License Type"]);
            EngineVolume = int.Parse(i_FilledUniqueData["Engine Volume"]);
            if (EngineVolume < 0)
            {
                throw new ValueOutOfRangeException(0);
            }
            
        }

    }

}
