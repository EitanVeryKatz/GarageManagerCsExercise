using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
       private FuelEngine m_engine = new FuelEngine(FuelEngine.e_FuelTypes.Octan98,48);

        public FuelCar(string i_ModelName, string i_LicenseID)
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
    }
    
}
