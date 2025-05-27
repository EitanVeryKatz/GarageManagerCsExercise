using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private readonly FuelEngine r_engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 5.8f);

        public FuelMotorcycle(string i_licenseID, string i_modelName) : base(i_modelName, i_licenseID)
        {

        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return r_engine.FuelType;
        }

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

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = r_engine.FuelType.ToString();
            vehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_energySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            r_engine.Refuel(i_energySourceAmountToAdd, i_fuelType);
        }

    }

}