﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private readonly FuelEngine engine = new FuelEngine(FuelEngine.e_FuelTypes.Soler, 135);
        private const int k_NumOfWheels = 12;
        private const float k_MaximunWheelAirPressure = 27;
        private float m_currentFuelAmount;

        public Truck(string i_ModelName, string i_LicenseID) : base(i_ModelName, i_LicenseID)
        {
            base.m_Wheels = new Wheel[k_NumOfWheels];
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(k_MaximunWheelAirPressure);
            }

        }

    }
}