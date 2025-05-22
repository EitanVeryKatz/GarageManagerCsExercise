using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        private readonly Dictionary<string, VehicleDataAndStatus> m_vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(Vehicle i_newVehicle)
        {
            VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_newVehicle);
            m_vehicles.Add(i_newVehicle.r_LicenseID, newVehicle);
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
        
        void fillAirInVehicle(string i_LicanseIdOfVehicle)
        {
            m_vehicles[i_LicanseIdOfVehicle].FillAirInAllTiresOfVehicle();
        }

        private class VehicleDataAndStatus
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
            
            public VehicleDataAndStatus(Vehicle i_vehicle)
            {
                r_vehicle = i_vehicle;
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
    }
}
