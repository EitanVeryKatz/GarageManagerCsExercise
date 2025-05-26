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
        public enum eVehicleStatuses
        {
            WorkInProgress,
            WorkFinished,
            Paid
        }

        private readonly Dictionary<string, VehicleDataAndStatus> m_Vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentFuelAmount = 0)
        {
            VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone, i_CurrentFuelAmount);
            m_Vehicles.Add(newVehicle.LicenseId, newVehicle);
        }

        public void UpdateTireInfoForNewVehicle(string i_licenseID, string[,] wheelData)
        {
            m_Vehicles[i_licenseID].SetTireInfo(wheelData);
        }

        public void ChangeVehicleStatus(string i_licanseIdOfVehicle, eVehicleStatuses i_newStatus)
        {
            m_Vehicles[i_licanseIdOfVehicle].Status = i_newStatus;
        }

        public int GetAmountOfTires(string i_vehicleId)
        {
            return m_Vehicles[i_vehicleId].TireCount;
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage()
        {
            return m_Vehicles.Keys.ToList();
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage(string i_statusStr)
        {
            eVehicleStatuses status = eVehicleStatuses.WorkInProgress;
            List<string> resaultList = new List<string>();

            switch (i_statusStr)
            {
                case ("Work in progress"):
                    status = eVehicleStatuses.WorkInProgress;
                    break;
                case ("Work finished"):
                    status = eVehicleStatuses.WorkFinished;
                    break;
                case ("Paid"):
                    status = eVehicleStatuses.Paid;
                    break;
                default:
                    throw new ArgumentException();
            }

            foreach (string licanceId in m_Vehicles.Keys)
            {
                if (m_Vehicles[licanceId].Status == status)
                {
                    resaultList.Add(licanceId);
                }

            }

            return resaultList;
        }

        public void FillAirInVehicle(string i_LicanseIdOfVehicle)
        {
            m_Vehicles[i_LicanseIdOfVehicle].FillAirInAllTiresOfVehicle();
        }

        public string[] GetUniqueDataMembersOfVehicle(string i_vehicleId)
        {
            return m_Vehicles[i_vehicleId].UniqueDataMembers;
        }

        public void RefuelVehicle(string i_vehicleId, string i_fuelTypeStr, float i_fuelAmountToAdd)
        {
            eFuelTypes fuelType;
            switch (i_fuelTypeStr)
            {
                case ("Octan98"):
                    fuelType = eFuelTypes.Octan98;
                    break;
                case ("Octan96"):
                    fuelType = eFuelTypes.Octan96;
                    break;
                case ("Octan95"):
                    fuelType = eFuelTypes.Octan95;
                    break;
                case ("Soler"):
                    fuelType = eFuelTypes.Soler;
                    break;
                default:
                    throw new ArgumentException();
            }

            m_Vehicles[i_vehicleId].Refuel(fuelType, i_fuelAmountToAdd);
        }

        public bool IsVehicleInGarage(string i_vehicleId)
        {
            return m_Vehicles.ContainsKey(i_vehicleId);
        }

        public void RechargeVehicle(string i_vehicleId, float i_minutesToCharge)
        {
            m_Vehicles[i_vehicleId].Recharge(i_minutesToCharge);
        }

        public Dictionary<string, string> GetAllDataForVehicle(string i_vehicleId)
        {
            return m_Vehicles[i_vehicleId].GetAllDataForVehicle();
        }

        public void SetUniqueMembers(string i_licenseID, Dictionary<string, string> i_FilledUniqueData)
        {
            m_Vehicles[i_licenseID].SetUniqueMembers(i_FilledUniqueData);
        }

        private void setEnergyPrecentageForVehicle(string i_vehicleId, float i_newEnergyPrecentage)
        {
            m_Vehicles[i_vehicleId].SetEnergyPrecentage(i_newEnergyPrecentage);
        }

        public void GetVehiclesFromFile()
        {
            string[] allVehiclesInDB = File.ReadAllLines("Vehicles.db");
            foreach (string line in allVehiclesInDB)
            {
                if (line.StartsWith("*"))
                {
                    break;
                }
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

                AddNewVehicle(VehicleType, LicenceId, ModelName, OwnerName, OwnerPhone, CurrentFuelAmount);
                string[] uniqueData = GetUniqueDataMembersOfVehicle(LicenceId);
                Dictionary<string, string> FilledUniqueData = new Dictionary<string, string>();

                for (int i = 0; i < uniqueData.Length; i++)
                {
                    FilledUniqueData[uniqueData[i]] = VehicleData[8 + i];
                }

                int numOfTires = GetAmountOfTires(LicenceId);
                string[,] wheelData = new string[numOfTires, 2];

                for (int i = 0; i < numOfTires; i++)
                {
                    wheelData[i, 0] = tierModel;
                    wheelData[i, 1] = CurrentAirPressure;
                }

                UpdateTireInfoForNewVehicle(LicenceId, wheelData);
                setEnergyPrecentageForVehicle(LicenceId, float.Parse(energyPrecentage));
                SetUniqueMembers(LicenceId, FilledUniqueData);

            }

        }

    }

}
