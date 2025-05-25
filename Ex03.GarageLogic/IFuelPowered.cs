using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ex03.GarageLogic
{
    public interface IFuelPowered
    {
        void Refuel(FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd);

        FuelEngine.e_FuelTypes GetFuelType();
    }
}
