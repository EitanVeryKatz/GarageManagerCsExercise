using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private readonly FuelEngine.e_FuelTypes r_FuelType = FuelEngine.e_FuelTypes.Octan98;
        private readonly float r_MaxFuelCapacity = 48f;
        private float m_CurrentFuelAmount;

        public FuelCar(string i_ModelName, string i_LicenseID)
            : base(i_ModelName, i_LicenseID)
        {
            m_Engine = new FuelEngine(r_FuelType, r_MaxFuelCapacity);
            m_CurrentFuelAmount = 0f;
        }

        public void Refuel(float i_AmountToAdd, FuelEngine.e_FuelTypes i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Fuel type does not match the car's required fuel type.");
            }

            if (i_AmountToAdd <= 0)
            {
                throw new ArgumentException("Amount to add must be positive.");
            }

            if (m_CurrentFuelAmount + i_AmountToAdd > r_MaxFuelCapacity)
            {
                throw new ValueOutOfRangeException(0, r_MaxFuelCapacity - m_CurrentFuelAmount, "Fuel amount exceeds tank capacity.");
            }

            ((FuelEngine)m_Engine).Refuel(i_AmountToAdd, i_FuelType);
            m_CurrentFuelAmount += i_AmountToAdd;
        }

        public FuelEngine.e_FuelTypes FuelType
        {
            get { return r_FuelType; }
        }

        public float CurrentFuelAmount
        {
            get { return m_CurrentFuelAmount; }
        }

        public float MaxFuelCapacity
        {
            get { return r_MaxFuelCapacity; }
        }
    }
    
}
