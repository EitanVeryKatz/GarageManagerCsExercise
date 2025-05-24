using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle:Motorcycle, IElectric
    {
        ElectricEngine m_engine = new ElectricEngine(1);

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        void IElectric.Recharge(float i_minutesToCharge)
        {
            m_engine.ChargeBattery(i_minutesToCharge/60);
        }

        protected override float EnergySourcePrecentage
        {
            get
            {

                return (m_engine.MinutesLeftInBattery / m_engine.MaxHoursOfUsage) * 100;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Battery Precentage"] = string.Format("{0}%", EnergySourcePrecentage);

            return vehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            m_engine.MinutesLeftInBattery = float.Parse(i_FilledUniqueData["Minutes Left in Battery"]);
        }

    }

}
