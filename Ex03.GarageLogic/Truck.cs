using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private readonly FuelEngine engine = new FuelEngine(FuelEngine.e_FuelTypes.Soler, 135);
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;
        private float m_currentFuelAmount;

        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStatus m_Status;

        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        public Truck(string i_ModelName, string i_LicenseID, string i_OwnerName, string i_OwnerPhone)
            : base(i_ModelName, i_LicenseID)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Status = eVehicleStatus.InRepair;
            m_currentFuelAmount = 0f;

            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }
        }

        public float CurrentFuelAmount
        {
            get { return m_currentFuelAmount; }
            set { m_currentFuelAmount = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
        }

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
    }
}