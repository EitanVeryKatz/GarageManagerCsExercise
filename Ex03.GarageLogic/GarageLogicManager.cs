using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager:VehicleCreator
    {
        private readonly Dictionary<string, VehicleDataAndStatus> m_vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(Vehicle i_newVehicle)
        {
        //    VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_newVehicle);
           // m_vehicles.Add(i_newVehicle.r_LicenseID, newVehicle);
        }

        public void ChangeVehicleStatus(string i_licanseIdOfVehicle, e_StatusOfVehicleInGarage i_newStatus)
        {
            m_vehicles[i_licanseIdOfVehicle].Status = i_newStatus;
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage()
        {
            return m_vehicles.Keys.ToList();
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage(e_StatusOfVehicleInGarage i_status)
        {
            List<string> resaultList = new List<string>();

            foreach (string licanceId in m_vehicles.Keys) 
            {
                if (m_vehicles[licanceId].Status==i_status)
                {
                    resaultList.Add(licanceId);
                }

            }

            return resaultList;
        }
        
        public void FillAirInVehicle(string i_LicanseIdOfVehicle)
        {
            m_vehicles[i_LicanseIdOfVehicle].FillAirInAllTiresOfVehicle();
        }

        public class VehicleDataAndStatus
        {
            private readonly Vehicle r_vehicle;

            private e_StatusOfVehicleInGarage m_status = e_StatusOfVehicleInGarage.WorkInProgress;
            public string OwnerName { get; private set; }
            public string OwnerPhoneNumber { get; private set; }
            public e_StatusOfVehicleInGarage StatusOfVehicleInGarage
            {
                get
                {
                    return m_status;
                }
                set
                {
                    m_status = value;
                }

            }
            
            public VehicleDataAndStatus(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentFuelAmount = 0)

            {
                r_vehicle = CreateVehicle(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone, i_CurrentFuelAmount);
                OwnerName = i_OwnerName;
                OwnerPhoneNumber = i_OwnerPhone;
                if(r_vehicle is IFuelPowered fuelPoweredVehicle)
                {
                    fuelPoweredVehicle.Refuel(fuelPoweredVehicle.GetFuelType(),i_CurrentFuelAmount);
                }
            }

            public e_StatusOfVehicleInGarage Status
            {
                get
                {
                    return m_status;
                }
                set 
                {
                    m_status = value;
                }

            }

            public void FillAirInAllTiresOfVehicle()
            {
                r_vehicle.FillAirInTires();
            }     
            
            public void Refuel(FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd)
            {
                r_vehicle.Refuel(i_fuelType, i_fuelAmountToAdd);
            }

            public void Recharge(int i_munitesToCharge)
            {
                r_vehicle.Recharge(i_munitesToCharge);
            }
        }

        public void RefuelVehicle(string i_vehicleId, FuelEngine.e_FuelTypes i_fuelType, float i_fuelAmountToAdd)
        {
            m_vehicles[i_vehicleId].Refuel(i_fuelType, i_fuelAmountToAdd);
        }

        public bool IsVehicleInGarage(string i_vehicleId)
        {
            return m_vehicles.ContainsKey(i_vehicleId);
        }

        public enum e_StatusOfVehicleInGarage
        {
            WorkInProgress,
            WorkFinished,
            Paid
        }

        public void RechargeVehicle(string i_vehicleId, int i_minutesToCharge)
        {
            m_vehicles[i_vehicleId].Recharge(i_minutesToCharge);
        }
    }
}
