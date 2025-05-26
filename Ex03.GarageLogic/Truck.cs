//VehicleCreator.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;

        private readonly FuelEngine m_Engine = new FuelEngine(Vehicle.eFuelTypes.Soler, 135);

        public bool HoldsDangerousMaterial { get; set; }

        public float CargoVolume { get; set; }

        internal override float EnergySourcePercentage
        {
            get
            {

                return (m_Engine.CurrentFuelAmount / m_Engine.MaxFuelCapacity) * 100;
            }
            set
            {
                m_Engine.CurrentFuelAmount = (value / 100) * m_Engine.MaxFuelCapacity;
            }

        }
        public Truck(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];

            UniqueDataMembers.Add("Holds Dangerous Material");
            UniqueDataMembers.Add("Cargo Volume");
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

        }

        public float CurrentFuelAmount
        {
            get
            {
                return m_Engine.CurrentFuelAmount;
            }

            private set
            {
                m_Engine.CurrentFuelAmount = value;
            }

        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return m_Engine.FuelType;
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = base.GetAllDataForVehicle();

            VehicleData["Fuel Type"] = "Soler";
            VehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePercentage);
            VehicleData["Holds Dangerous Material"] = HoldsDangerousMaterial.ToString();
            VehicleData["Cargo Volume"] = CargoVolume.ToString();

            return VehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            CargoVolume = float.Parse(i_FilledUniqueData["Cargo Volume"]);
            HoldsDangerousMaterial = bool.Parse(i_FilledUniqueData["Holds Dangerous Material"]);
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            m_Engine.Refuel(i_EnergySourceAmountToAdd, i_fuelType);
        }

    }

}