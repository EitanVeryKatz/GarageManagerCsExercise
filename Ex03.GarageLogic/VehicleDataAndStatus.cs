using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleDataAndStatus : VehicleCreator
    {
        private readonly Vehicle r_vehicle;
        private eVehicleStatuses m_status = eVehicleStatuses.WorkInProgress;
        public string OwnerName { get; private set; }
        public string OwnerPhoneNumber { get; private set; }
        public string LicenseId
        {
            get
            {
                return r_vehicle.r_LicenseID;
            }

        }

        public eVehicleStatuses Status
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

        public eVehicleStatuses StatusOfVehicleInGarage
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

        public int TireCount
        {
            get
            {
                return r_vehicle.TireCount;
            }

        }

        public string[] UniqueDataMembers
        {
            get
            {
                string[] dataMembers = new string[r_vehicle.m_uniqueDataMembers.Count];
                r_vehicle.m_uniqueDataMembers.CopyTo(dataMembers);

                return dataMembers;
            }

        }

        public VehicleDataAndStatus(string i_vehicleType, string i_licenseID, string i_modelName, string i_ownerName, string i_ownerPhone)
        {
            r_vehicle = CreateVehicle(i_vehicleType, i_licenseID, i_modelName, i_ownerName, i_ownerPhone);
            OwnerName = i_ownerName;
            OwnerPhoneNumber = i_ownerPhone;
        }

        public void FillAirInAllTiresOfVehicle()
        {
            r_vehicle.FillAirInTires();
        }

        public void Refuel(Vehicle.eFuelTypes i_fuelType, float i_fuelAmountToAdd)
        {
            r_vehicle.AddToEnergySource(i_fuelAmountToAdd, i_fuelType);
        }

        public void Recharge(float i_minutesToCharge)
        {
            r_vehicle.AddToEnergySource(i_minutesToCharge);
        }

        internal void SetTireInfo(string[,] i_tireInfo)
        {
            r_vehicle.SetTireInfo(i_tireInfo);
        }

        internal Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = r_vehicle.GetAllDataForVehicle();

            VehicleData["Owner Name"] = OwnerName;
            VehicleData["Owner Phone"] = OwnerPhoneNumber;
            VehicleData["Status"] = m_status.ToString();

            return VehicleData;
        }

        internal void SetUniqueMembers(Dictionary<string, string> i_filledUniqueData)
        {
            r_vehicle.SetUniqueMembers(i_filledUniqueData);
        }

        internal void SetEnergyPrecentage(float i_newEnergyPrecentage)
        {
            if (i_newEnergyPrecentage < 0 || i_newEnergyPrecentage > 100)
            {
                throw new ArgumentOutOfRangeException("Energy source precentage must be in the range 0 - 100.");
            }

            r_vehicle.EnergySourcePercentage = i_newEnergyPrecentage;
        }

        public enum eVehicleStatuses
        {
            WorkInProgress,
            WorkFinished,
            Paid
        }

    }

}