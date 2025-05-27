using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaximunWheelAirPressure = 30;

        public enum eMotorcycleLicenseTypes
        {
            A,
            A2,
            AB,
            B2
        }

        public eMotorcycleLicenseTypes License { get; set; }

        public int EngineVolume { get; set; }

        public Motorcycle(string i_modelName, string i_licenseID) : base(i_modelName, i_licenseID)
        {
            base.m_wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

            m_uniqueDataMembers.Add("License Type");
            m_uniqueDataMembers.Add("Engine Volume");
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["License Type"] = License.ToString();
            vehicleData["Engine Volume"] = EngineVolume.ToString() + " cc";

            return vehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_filledUniqueData)
        {
            License = (eMotorcycleLicenseTypes)Enum.Parse(typeof(eMotorcycleLicenseTypes), i_filledUniqueData["License Type"]);
            EngineVolume = int.Parse(i_filledUniqueData["Engine Volume"]);
            if (EngineVolume < 0)
            {
                throw new ValueOutOfRangeException(0);
            }
            
        }

    }

}
