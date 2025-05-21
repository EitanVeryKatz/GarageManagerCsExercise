using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    interface IFuelUsing
    {
        e_FuelTypes FuelType { get;}
        float CurrentFuelAmount { get; set; }
        float MaxFuelAmount { get;}

        void fillFuel(float i_fuelAmount, e_FuelTypes i_fuelType);
    }

    public enum e_FuelTypes
    {
        Soler,
        Octan95,
        Octan96,
        Octan98,
    }
}
