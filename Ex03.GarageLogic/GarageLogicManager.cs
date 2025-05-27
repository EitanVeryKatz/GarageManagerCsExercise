using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {

        private readonly Dictionary<string, VehicleDataAndStatus> m_Vehicles = new Dictionary<string, VehicleDataAndStatus>();

        public void AddNewVehicle(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_currentEnergySourcePrecentage = 0)
        {
            VehicleDataAndStatus newVehicle = new VehicleDataAndStatus(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone);
            newVehicle.SetEnergyPrecentage(i_currentEnergySourcePrecentage);
            m_Vehicles.Add(newVehicle.LicenseId, newVehicle);
        }

        public void UpdateTireInfoForNewVehicle(string i_licenseID, string[,] wheelData)
        {
            m_Vehicles[i_licenseID].SetTireInfo(wheelData);
        }

        public void ChangeVehicleStatus(string i_licanseIdOfVehicle, string i_newStatus ="WorkInProgress")
        {
            m_Vehicles[i_licanseIdOfVehicle].Status = (VehicleDataAndStatus.eVehicleStatuses)Enum.Parse(typeof(VehicleDataAndStatus.eVehicleStatuses),i_newStatus);
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
            
            List<string> resaultList = new List<string>();
            if (Enum.TryParse<VehicleDataAndStatus.eVehicleStatuses>(i_statusStr, out VehicleDataAndStatus.eVehicleStatuses o_status)) {

                foreach (string licanceId in m_Vehicles.Keys)
                {
                    if (m_Vehicles[licanceId].Status == o_status)
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
            bool isValidFuelType = Enum.TryParse<Vehicle.eFuelTypes>(i_fuelTypeStr,out Vehicle.eFuelTypes fuelType);
            if (isValidFuelType == false)
            {
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
                string[] VehicleData = line.Split(',');
                string VehicleType = VehicleData[(int)eDbIndex.VehicleType];
                string LicenceId = VehicleData[(int)eDbIndex.LicenceID];
                string ModelName = VehicleData[(int)eDbIndex.ModelName];
                string energyPrecentage = VehicleData[(int)eDbIndex.EnergyPrecentage];
                string tierModel = VehicleData[(int)eDbIndex.TireModel];
                string CurrentAirPressure = VehicleData[(int)eDbIndex.CurrentAirPressure];
                string OwnerName = VehicleData[(int)eDbIndex.OwnerName];
                string OwnerPhone = VehicleData[(int)eDbIndex.OwnerPhone];

                if (IsVehicleInGarage(LicenceId))
                {
                    ChangeVehicleStatus(LicenceId);
                }
                else
                {
                    AddNewVehicle(VehicleType, LicenceId, ModelName, OwnerName, OwnerPhone, CurrentFuelAmount);
                    string[] uniqueData = GetUniqueDataMembersOfVehicle(LicenceId);
                    Dictionary<string, string> FilledUniqueData = new Dictionary<string, string>();

                    for (int i = 0; i < uniqueData.Length; i++)
                    {
                        FilledUniqueData[uniqueData[i]] = VehicleData[(int)eDbIndex.StartOfUniqueData + i];
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

}
