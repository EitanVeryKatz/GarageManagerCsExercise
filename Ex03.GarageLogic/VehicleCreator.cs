using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class VehicleCreator
    {
        public  List<string> SupportedTypes
        {
            get { return new List<string> { "FuelCar", "ElectricCar", "FuelMotorcycle", "ElectricMotorcycle", "Truck" }; }
        }

        public  Vehicle CreateVehicle(string i_vehicleType, string i_licenseID, string i_modelName, string i_ownerName, string i_ownerPhone, float i_CurrentFuelAmount = 0)
        {
            Vehicle newVehicle = null;

            switch (i_vehicleType)
            {
                case "FuelCar":
                    newVehicle = new FuelCar(i_licenseID, i_modelName);
                    break;
                case "ElectricCar":
                    newVehicle = new ElectricCar(i_licenseID, i_modelName);
                    break;
                case "FuelMotorcycle":
                    newVehicle = new FuelMotorcycle(i_licenseID, i_modelName);
                    break;
                case "ElectricMotorcycle":
                    newVehicle = new ElectricMotorcycle(i_licenseID, i_modelName);
                    break;
                case "Truck":
                    newVehicle = new Truck(i_licenseID, i_modelName);
                    break;
                default:
                    throw new ArgumentException();
            }

            return newVehicle;
        }

    }

}
