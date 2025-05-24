using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        private readonly Dictionary<string, VehicleDataAndStatus> m_vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone ,float i_CurrentFuelAmount = 0)
        {
            
            VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_VehicleType,i_LicenseID,i_ModelName,i_OwnerName,i_OwnerPhone,i_CurrentFuelAmount);
            m_vehicles.Add(newVehicle.LicenseId, newVehicle);
        }

        public void UpdateTireInfoForNewVehicle(string i_licenseID, string[,] wheelData)
        {
            m_vehicles[i_licenseID].SetTireInfo(wheelData);
        }
        public void ChangeVehicleStatus(string i_licanseIdOfVehicle, e_StatusOfVehicleInGarage i_newStatus)
        {
            m_vehicles[i_licanseIdOfVehicle].Status = i_newStatus;
        }

        public int GetAmountOfTires(string i_vehicleId)
        {
            return m_vehicles[i_vehicleId].TireCount;
        }
        public List<string> GetAllLicanseNumbersOfVehiclesInGarage()
        {
            return m_vehicles.Keys.ToList();
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage(string i_statusStr)
        {
            e_StatusOfVehicleInGarage status = e_StatusOfVehicleInGarage.WorkInProgress;
            List<string> resaultList = new List<string>();

            switch (i_statusStr)
            {
                case ("Work in progress"):
                    status = e_StatusOfVehicleInGarage.WorkInProgress;
                    break;
                case ("Work finished"):
                    status = e_StatusOfVehicleInGarage.WorkFinished;
                    break;
                case ("Paid"):
                    status = e_StatusOfVehicleInGarage.Paid;
                    break;
                default:
                    throw new ArgumentException();
            }
            
            foreach (string licanceId in m_vehicles.Keys) 
            {
                if (m_vehicles[licanceId].Status==status)
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

        public string[] GetUniqueDataMembersOfVehicle(string i_vehicleId)
        {
            return m_vehicles[i_vehicleId].UniqueDataMembers;
        }
        
        public void RefuelVehicle(string i_vehicleId, string i_fuelTypeStr, float i_fuelAmountToAdd)
        {
            e_FuelTypes fuelType;
            switch (i_fuelTypeStr)
            {
                case ("Octan98"):
                    fuelType = e_FuelTypes.Octan98;
                        break;
                case ("Octan96"):
                    fuelType = e_FuelTypes.Octan96;
                    break;
                case ("Octan95"):
                    fuelType = e_FuelTypes.Octan95;
                    break;
                case ("Soler"):
                    fuelType = e_FuelTypes.Soler;
                    break;
                default:
                    throw new ArgumentException();
            }
            m_vehicles[i_vehicleId].Refuel(fuelType, i_fuelAmountToAdd);
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

        public void RechargeVehicle(string i_vehicleId, float i_minutesToCharge)
        {
            m_vehicles[i_vehicleId].Recharge(i_minutesToCharge);
        }

        public Dictionary<string,string> GetAllDataForVehicle(string i_vehicleId)
        {
            return m_vehicles[i_vehicleId].GetAllDataForVehicle();
        }

        public void SetUniqueMembers(string i_licenseID,Dictionary<string,string> i_FilledUniqueData)
        {
            m_vehicles[i_licenseID].SetUniqueMembers(i_FilledUniqueData);
        }

        private void SetEnergyPrecentageForVehicle(string i_vehicleId,float i_newEnergyPrecentage)
        {
            m_vehicles[i_vehicleId].SetEnergyPrecentage(i_newEnergyPrecentage);
        }

        public void GetVehiclesFromFile(string i_fileName)
        {           
            string[] allVehiclesInDB = File.ReadAllLines(i_fileName);
            foreach (string line in allVehiclesInDB)
            {   
                float CurrentFuelAmount = 0;
                string[] VehicleData = line.Split(',');
                string VehicleType = VehicleData[0];
                string LicenceId = VehicleData[1];
                string ModelName = VehicleData[2];
                string energyPrecentage = VehicleData[3];
                string tierModel = VehicleData[4];
                string CurrentAirPressure = VehicleData[5];
                string OwnerName = VehicleData[6];
                string OwnerPhone = VehicleData[7];
                string[] uniqueData = GetUniqueDataMembersOfVehicle(LicenceId);
                Dictionary<string,string> FilledUniqueData = new Dictionary<string,string>();
                for(int i = 0; i < uniqueData.Length; i++)
                {
                    FilledUniqueData[uniqueData[i]] = VehicleData[8+i];
                }
                int numOfTires = GetAmountOfTires(LicenceId);
                string[,] wheelData = new string[numOfTires,2];

                for (int i = 0; i < numOfTires; i++)
                {
                    wheelData[i, 0] = tierModel;
                    wheelData[i, 1] = CurrentAirPressure;
                }


                
                AddNewVehicle(VehicleType, LicenceId, ModelName, OwnerName, OwnerPhone, CurrentFuelAmount);
                UpdateTireInfoForNewVehicle(LicenceId, wheelData);
                SetEnergyPrecentageForVehicle(LicenceId, float.Parse(energyPrecentage));
                SetUniqueMembers(LicenceId, FilledUniqueData);
                
            }
        }

    }

}
