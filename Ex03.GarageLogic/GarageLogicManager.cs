using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        private readonly Dictionary<string, VehicleEntry> r_Vehicles = new Dictionary<string, VehicleEntry>();

        public void AddNewVehicle(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentEnergySourcePercentage = 0)
        {
            VehicleEntry newVehicle = new VehicleEntry(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone);

            newVehicle.SetEnergyPercentage(i_CurrentEnergySourcePercentage);
            r_Vehicles.Add(newVehicle.LicenseId, newVehicle);
        }

        public void UpdateTireInfoForNewVehicle(string i_LicenseID, string[,] i_WheelData)
        {
            r_Vehicles[i_LicenseID].SetTireInfo(i_WheelData);
        }

        public void ChangeVehicleStatus(string i_LicenseID, string i_newStatus ="WorkInProgress")
        {
            r_Vehicles[i_LicenseID].StatusOfVehicle = (VehicleEntry.eVehicleStatuses)Enum.Parse(typeof(VehicleEntry.eVehicleStatuses),i_newStatus);
        }

        public int GetAmountOfTires(string i_LicenseID)
        {
            return r_Vehicles[i_LicenseID].TireCount;
        }

        public List<string> GetAllLicenseIDsOfVehicles()
        {
            return r_Vehicles.Keys.ToList();
        }

        public List<string> GetLicenseIDsOfVehiclesByStatus(string i_statusStr)
        {
            
            List<string> resaultList = new List<string>();

            if (Enum.TryParse<VehicleEntry.eVehicleStatuses>(i_statusStr, out VehicleEntry.eVehicleStatuses o_status)) 
            {
                foreach (string licanceId in r_Vehicles.Keys)
                {
                    if (r_Vehicles[licanceId].StatusOfVehicle == o_status)
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

        public void FillAirInVehicle(string i_LicenseID)
        {
            r_Vehicles[i_LicenseID].FillAirInAllTires();
        }

        public string[] GetUniqueDataMembersOfVehicle(string i_LicenseID)
        {
            return r_Vehicles[i_LicenseID].UniqueDataMembers;
        }

        public void RefuelVehicle(string i_LicenseID, string i_FuelTypeStr, float i_FuelAmountToAdd)
        {
            bool isValidFuelType = Enum.TryParse<Vehicle.eFuelTypes>(i_FuelTypeStr,out Vehicle.eFuelTypes o_FuelType);

            if (isValidFuelType == false)
            {
                throw new ArgumentException();
            }
                    
            r_Vehicles[i_LicenseID].Refuel(o_FuelType, i_FuelAmountToAdd);
        }

        public bool IsVehicleInGarage(string i_LicenseID)
        {
            return r_Vehicles.ContainsKey(i_LicenseID);
        }

        public void RechargeVehicle(string i_LicenseID, float i_MinutesToCharge)
        {
            r_Vehicles[i_LicenseID].Recharge(i_MinutesToCharge);
        }

        public Dictionary<string, string> GetAllDataForVehicle(string i_LicenseID)
        {
            return r_Vehicles[i_LicenseID].GetAllDataForVehicle();
        }

        public void SetUniqueMembers(string i_LicenseID, Dictionary<string, string> i_FilledUniqueData)
        {
            r_Vehicles[i_LicenseID].SetUniqueMembers(i_FilledUniqueData);
        }

        private void setEnergyPercentageForVehicle(string i_LicenseID, float i_NewEnergyPercentage)
        {
            r_Vehicles[i_LicenseID].SetEnergyPercentage(i_NewEnergyPercentage);
        }

        private enum eDbIndex
        {
            VehicleType,
            LicenceID,
            ModelName,
            EnergyPercentage,
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
                string energyPercentage = vehicleData[(int)eDbIndex.EnergyPercentage];
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
                    setEnergyPercentageForVehicle(licenceId, float.Parse(energyPercentage));
                    SetUniqueMembers(licenceId, filledUniqueData);
                }

            }

        }

    }

}
