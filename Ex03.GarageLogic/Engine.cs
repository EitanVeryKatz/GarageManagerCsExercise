using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public float MaxEnergyCapacity { get; protected set; }
        public float CurrentEnergyAmount { get; protected set; }

        public abstract void AddEnergy(float i_AmountToAdd); // add fuel or charge battery
    }
}
