using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;

        private readonly FuelEngine r_Engine = new FuelEngine(Vehicle.eFuelTypes.Soler, 135);

        public bool HoldsDangerousMaterial { get; set; }

        public float CargoVolume { get; set; }

        internal override float EnergySourcePercentage
        {
            get
            {

                return (r_Engine.CurrentFuelAmount / r_Engine.MaxFuelCapacity) * 100;
            }
            set
            {
                r_Engine.CurrentFuelAmount = (value / 100) * r_Engine.MaxFuelCapacity;
            }

        }

        public Truck(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {
            m_UniqueDataMembers.Add("Holds Dangerous Material");
            m_UniqueDataMembers.Add("Cargo Volume");
            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

        }

        public float CurrentFuelAmount
        {
            get
            {
                return r_Engine.CurrentFuelAmount;
            }
            private set
            {
                r_Engine.CurrentFuelAmount = value;
            }

        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_Engine.FuelType;
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = base.GetAllDataForVehicle();

            VehicleData["Fuel Type"] = "Soler";
            VehicleData["Fuel Tank Percentage"] = string.Format("{0}%", EnergySourcePercentage);
            VehicleData["Holds Dangerous Material"] = HoldsDangerousMaterial.ToString();
            VehicleData["Cargo Volume"] = CargoVolume.ToString();

            return VehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            CargoVolume = float.Parse(i_FilledUniqueData["Cargo Volume"]);
            HoldsDangerousMaterial = bool.Parse(i_FilledUniqueData["Holds Dangerous Material"]);
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_FuelType = eFuelTypes.None)
        {
            r_Engine.Refuel(i_EnergySourceAmountToAdd, i_FuelType);
        }

    }

}