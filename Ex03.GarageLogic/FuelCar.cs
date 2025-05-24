using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car, IFuelPowered
    {
       private FuelEngine m_engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98,48);

        public FuelCar(string i_LicenseID, string i_ModelName)
            : base(i_ModelName, i_LicenseID)
        {
           
        }

        public void Refuel(float i_AmountToAdd, FuelEngine.e_FuelTypes i_FuelType)
        {
            if (i_FuelType != m_engine.FuelType)
            {
                throw new ArgumentException("Fuel type does not match the car's required fuel type.");
            }

            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("Amount to add must be positive.");
            }

            if (m_engine.CurrentFuelAmount + i_AmountToAdd > m_engine.MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, m_engine.MaxFuelCapacity - m_engine.CurrentFuelAmount, "Fuel amount exceeds tank capacity.");
            }

            m_engine.Refuel(i_AmountToAdd, i_FuelType);
            m_engine.CurrentFuelAmount += i_AmountToAdd;
        }

        public FuelEngine.e_FuelTypes FuelType
        {
            get { return m_engine.FuelType; }
        }

        public float CurrentFuelAmount
        {
            get { return m_engine.CurrentFuelAmount; }
        }

        public float MaxFuelCapacity
        {
            get { return m_engine.MaxFuelCapacity; }
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
            set
            {
                m_engine.CurrentFuelAmount = (value / 100) * m_engine.MaxFuelCapacity;
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
