//VehicleCreator.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle, IFuelPowered
    {
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;

        private readonly FuelEngine m_engine = new FuelEngine(FuelEngine.e_FuelTypes.Soler, 135);
        public bool HoldsDangerousMaterial {  get; set; }
        public float CargoVolume {  get; set; }

        protected override float EnergySourcePrecentage
        {
            get
            {

                return (m_engine.CurrentFuelAmount / m_engine.MaxFuelCapacity) * 100;
            }
        }

        

        public Truck(string i_LicenseID, string i_ModelName)
            : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];

            uniqueDataMembers.Add("Cargo Volume");
            uniqueDataMembers.Add("Holds Dangerous Material");
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

        }

        public float CurrentFuelAmount
        {
            get 
            {
                return m_engine.CurrentFuelAmount; 
            }
            private set 
            {
                m_engine.CurrentFuelAmount = value; 
            }

        }

        void IFuelPowered.Refuel(FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd)
        {
            m_engine.Refuel(i_fuelAmountToAdd, i_fuelType);
        }

        FuelEngine.e_FuelTypes IFuelPowered.GetFuelType()
        {
            return m_engine.FuelType;
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = base.GetAllDataForVehicle();

            VehicleData["Fuel Type"] = "Soler";
            VehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePrecentage);
            VehicleData["Holds Dangerous Material"] = HoldsDangerousMaterial.ToString();
            VehicleData["Cargo Volume"] = CargoVolume.ToString();

            return VehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            m_engine.CurrentFuelAmount = float.Parse(i_FilledUniqueData["Current Fuel in Tank"]);
            CargoVolume = float.Parse(i_FilledUniqueData["Cargo Volume"]);
            HoldsDangerousMaterial = bool.Parse(i_FilledUniqueData["Holds Dangerous Material"]);
        }

    }

}