using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle:Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaximunWheelAirPressure = 30;

        public e_MotorcycleLicanceType Licance {  get; set; }
        public int EngineDisplacementInCubicCentimeters {  get; set; }

        public Motorcycle(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }
            uniqueDataMembers.Add("Licanse Type");
            uniqueDataMembers.Add("Engine Volume");
            

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["LicanseType"] = Licance.ToString();
            vehicleData["Engine Volume"] = EngineDisplacementInCubicCentimeters.ToString() + " cc";

            return vehicleData;
        }

        public enum e_MotorcycleLicanceType 
        {
            A,
            A2,
            AB,
            B2
        }

    }

}
