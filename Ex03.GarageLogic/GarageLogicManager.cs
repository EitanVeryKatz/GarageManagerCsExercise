using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        private readonly Dictionary<string, VehicleDataAndStatus> r_vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(string i_vehicleType, string i_licenseID, string i_modelName, string i_ownerName, string i_ownerPhone, float i_currentEnergySourcePrecentage = 0)
        {
            VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_vehicleType, i_licenseID, i_modelName, i_ownerName, i_ownerPhone);

            newVehicle.SetEnergyPrecentage(i_currentEnergySourcePrecentage);
            r_vehicles.Add(newVehicle.LicenseId, newVehicle);
        }

        public void UpdateTireInfoForNewVehicle(string i_licenseID, string[,] wheelData)
        {
            r_vehicles[i_licenseID].SetTireInfo(wheelData);
        }

        public void ChangeVehicleStatus(string i_licanseID, string i_newStatus ="WorkInProgress")
        {
            r_vehicles[i_licanseID].Status = (VehicleDataAndStatus.eVehicleStatuses)Enum.Parse(typeof(VehicleDataAndStatus.eVehicleStatuses),i_newStatus);
        }

        public int GetAmountOfTires(string i_licenseID)
        {
            return r_vehicles[i_licenseID].TireCount;
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage()
        {
            return r_vehicles.Keys.ToList();
        }

        public List<string> GetAllLicanseNumbersOfVehiclesInGarage(string i_statusStr)
        {
            
            List<string> resaultList = new List<string>();

            if (Enum.TryParse<VehicleDataAndStatus.eVehicleStatuses>(i_statusStr, out VehicleDataAndStatus.eVehicleStatuses o_status)) 
            {
                foreach (string licanceId in r_vehicles.Keys)
                {
                    if (r_vehicles[licanceId].Status == o_status)
                    {
                        resaultList.Add(licanceId);
                    }

                } 

            }
            else
            {
                throw new ArgumentException();
            }

            return resaultList;
        }

        public void FillAirInVehicle(string i_licanseID)
        {
            r_vehicles[i_licanseID].FillAirInAllTiresOfVehicle();
        }

        public string[] GetUniqueDataMembersOfVehicle(string i_licenseID)
        {
            return r_vehicles[i_licenseID].UniqueDataMembers;
        }

        public void RefuelVehicle(string i_licenseID, string i_fuelTypeStr, float i_fuelAmountToAdd)
        {
            bool isValidFuelType = Enum.TryParse<Vehicle.eFuelTypes>(i_fuelTypeStr,out Vehicle.eFuelTypes fuelType);

            if (isValidFuelType == false)
            {
                throw new ArgumentException();
            }
                    
            r_vehicles[i_licenseID].Refuel(fuelType, i_fuelAmountToAdd);
        }

        public bool IsVehicleInGarage(string i_licenseID)
        {
            return r_vehicles.ContainsKey(i_licenseID);
        }

        public void RechargeVehicle(string i_licenseID, float i_minutesToCharge)
        {
            r_vehicles[i_licenseID].Recharge(i_minutesToCharge);
        }

        public Dictionary<string, string> GetAllDataForVehicle(string i_licenseID)
        {
            return r_vehicles[i_licenseID].GetAllDataForVehicle();
        }

        public void SetUniqueMembers(string i_licenseID, Dictionary<string, string> i_filledUniqueData)
        {
            r_vehicles[i_licenseID].SetUniqueMembers(i_filledUniqueData);
        }

        private void setEnergyPrecentageForVehicle(string i_licenseID, float i_newEnergyPrecentage)
        {
            r_vehicles[i_licenseID].SetEnergyPrecentage(i_newEnergyPrecentage);
        }

        private enum eDbIndex
        {
            VehicleType,
            LicenceID,
            ModelName,
            EnergyPrecentage,
            TireModel,
            CurrentAirPressure,
            OwnerName,
            OwnerPhone,
            StartOfUniqueData
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
                string[] vehicleData = line.Split(',');
                string vehicleType = vehicleData[(int)eDbIndex.VehicleType];
                string licenceId = vehicleData[(int)eDbIndex.LicenceID];
                string modelName = vehicleData[(int)eDbIndex.ModelName];
                string energyPrecentage = vehicleData[(int)eDbIndex.EnergyPrecentage];
                string tierModel = vehicleData[(int)eDbIndex.TireModel];
                string currentAirPressure = vehicleData[(int)eDbIndex.CurrentAirPressure];
                string ownerName = vehicleData[(int)eDbIndex.OwnerName];
                string ownerPhone = vehicleData[(int)eDbIndex.OwnerPhone];

                if (IsVehicleInGarage(licenceId))
                {
                    ChangeVehicleStatus(licenceId);
                }
                else
                {
                    AddNewVehicle(vehicleType, licenceId, modelName, ownerName, ownerPhone, CurrentFuelAmount);
                    string[] uniqueData = GetUniqueDataMembersOfVehicle(licenceId);
                    Dictionary<string, string> filledUniqueData = new Dictionary<string, string>();

                    for (int i = 0; i < uniqueData.Length; i++)
                    {
                        filledUniqueData[uniqueData[i]] = vehicleData[(int)eDbIndex.StartOfUniqueData + i];
                    }

                    int numOfTires = GetAmountOfTires(licenceId);
                    string[,] wheelData = new string[numOfTires, 2];

                    for (int i = 0; i < numOfTires; i++)
                    {
                        wheelData[i, 0] = tierModel;
                        wheelData[i, 1] = currentAirPressure;
                    }

                    UpdateTireInfoForNewVehicle(licenceId, wheelData);
                    setEnergyPrecentageForVehicle(licenceId, float.Parse(energyPrecentage));
                    SetUniqueMembers(licenceId, filledUniqueData);
                }

            }

        }

    }

}
