using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private readonly FuelEngine r_Engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 5.8f);

        public FuelMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_Engine.FuelType;
        }

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

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = r_Engine.FuelType.ToString();
            vehicleData["Fuel Tank Percentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_FuelType = eFuelTypes.None)
        {
            r_Engine.Refuel(i_EnergySourceAmountToAdd, i_FuelType);
        }

    }

}