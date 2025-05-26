using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private FuelEngine m_Engine = new FuelEngine(Vehicle.eFuelTypes.Octan98, 48);

        public FuelCar(string i_LicenseID, string i_ModelName) : base(i_ModelName, i_LicenseID)
        {

        }

        public void Refuel(float i_AmountToAdd, Vehicle.eFuelTypes i_FuelType)
        {
            if (i_FuelType != m_Engine.FuelType)
            {
                throw new ArgumentException("Fuel type does not match the car's required fuel type.");
            }

            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("Amount to add must be positive.");
            }

            if (m_Engine.CurrentFuelAmount + i_AmountToAdd > m_Engine.MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, m_Engine.MaxFuelCapacity - m_Engine.CurrentFuelAmount, "Fuel amount exceeds tank capacity.");
            }

            m_Engine.Refuel(i_AmountToAdd, i_FuelType);
            m_Engine.CurrentFuelAmount += i_AmountToAdd;
        }

        public Vehicle.eFuelTypes FuelType
        {
            get { return m_Engine.FuelType; }
        }

        public float CurrentFuelAmount
        {
            get { return m_Engine.CurrentFuelAmount; }
        }

        public float MaxFuelCapacity
        {
            get { return m_Engine.MaxFuelCapacity; }
        }

        internal override Vehicle.eFuelTypes GetFuelType()
        {
            return m_Engine.FuelType;
        }

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

        internal override Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> vehicleData = base.GetAllDataForVehicle();

            vehicleData["Fuel Type"] = m_Engine.FuelType.ToString();
            vehicleData["Fuel Tank Precentage"] = string.Format("{0}%", EnergySourcePercentage);

            return vehicleData;
        }

        public override void AddToEnergySource(float i_EnergySourceAmountToAdd, eFuelTypes i_fuelType = eFuelTypes.None)
        {
            m_Engine.Refuel(i_EnergySourceAmountToAdd, i_fuelType);
        }

    }

}
