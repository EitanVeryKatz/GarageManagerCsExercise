
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car,IElectric
    {
        private ElectricEngine m_engine = new ElectricEngine(4000000);
        public ElectricCar(string i_LicenseID, string i_ModelName)
            : base(i_ModelName, i_LicenseID)
        {
        }

        void IElectric.Recharge(float i_minutesToCharge)
        {
            m_engine.ChargeBattery(i_minutesToCharge / 60);
        }

        protected override float EnergySourcePrecentage
        {
            get
            {

                return (m_engine.CurrentAmountOfHoursLeftInBattery / m_engine.MaxHoursOfUsage) * 100;
            }

        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Battery Precentage"] = string.Format("{0}%", EnergySourcePrecentage);

            return vehicleData;
        }

    }
}