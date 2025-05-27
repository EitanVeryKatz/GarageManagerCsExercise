using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;

        private readonly FuelEngine r_engine = new FuelEngine(Vehicle.eFuelTypes.Soler, 135);

        public bool HoldsDangerousMaterial { get; set; }

        public float CargoVolume { get; set; }

        internal override float EnergySourcePercentage
        {
            get
            {

                return (r_engine.CurrentFuelAmount / r_engine.MaxFuelCapacity) * 100;
            }
            set
            {
                r_engine.CurrentFuelAmount = (value / 100) * r_engine.MaxFuelCapacity;
            }

        }

        public Truck(string i_licenseID, string i_modelName) : base(i_modelName, i_licenseID)
        {
            m_uniqueDataMembers.Add("Holds Dangerous Material");
            m_uniqueDataMembers.Add("Cargo Volume");
            base.m_wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

        }

        public float CurrentFuelAmount
        {
            get
            {
                return r_engine.CurrentFuelAmount;
            }
            private set
            {
                r_engine.CurrentFuelAmount = value;
            }

        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_engine.FuelType;
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

        internal override void SetUniqueMembers(Dictionary<string, string> i_filledUniqueData)
        {
            CargoVolume = float.Parse(i_filledUniqueData["Cargo Volume"]);
            HoldsDangerousMaterial = bool.Parse(i_filledUniqueData["Holds Dangerous Material"]);
        }

        public override void AddToEnergySource(float i_energySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            r_engine.Refuel(i_energySourceAmountToAdd, i_fuelType);
        }

    }

}