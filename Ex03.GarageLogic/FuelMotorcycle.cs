using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle:Motorcycle, IFuelPowered
    {
        private readonly FuelEngine m_engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98, 5.8f);
        public FuelMotorcycle(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {
            
        }
        
        void IFuelPowered.Refuel(FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd) 
        {
            m_engine.Refuel(i_fuelAmountToAdd, i_fuelType);
        }

        FuelEngine.e_FuelTypes IFuelPowered.GetFuelType()
        {
            return m_engine.FuelType;
        }

        protected override float EnergySourcePrecentage
        {
            get
            {

                return (m_engine.CurrentFuelAmount / m_engine.MaxFuelCapacity) * 100;
            }
        }

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = m_engine.FuelType.ToString();
            vehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePrecentage);

            return vehicleData;
        }

        internal override void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            m_engine.CurrentFuelAmount = float.Parse(i_FilledUniqueData["Current Fuel in Tank"]);
        }

    }

}
