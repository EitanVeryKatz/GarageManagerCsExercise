using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleEntry : VehicleCreator
    {
        private readonly Vehicle r_Vehicle;
        private eVehicleStatuses m_Status = eVehicleStatuses.WorkInProgress;
        public string OwnerName { get; private set; }
        public string OwnerPhoneNumber { get; private set; }
        public string LicenseId
        {
            get
            {
                return r_Vehicle.r_LicenseID;
            }

        }
        public eVehicleStatuses StatusOfVehicle
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }

        }

        public int TireCount
        {
            get
            {
                return r_Vehicle.TireCount;
            }

        }

        public string[] UniqueDataMembers
        {
            get
            {
                string[] dataMembers = new string[r_Vehicle.m_UniqueDataMembers.Count];
                r_Vehicle.m_UniqueDataMembers.CopyTo(dataMembers);

                return dataMembers;
            }

        }

        public VehicleEntry(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone)
        {
            r_Vehicle = CreateVehicle(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone);
            OwnerName = i_OwnerName;
            OwnerPhoneNumber = i_OwnerPhone;
        }

        public void FillAirInAllTires()
        {
            r_Vehicle.FillAirInTires();
        }

        public void Refuel(Vehicle.eFuelTypes i_FuelType, float i_FuelAmountToAdd)
        {
            r_Vehicle.AddToEnergySource(i_FuelAmountToAdd, i_FuelType);
        }

        public void Recharge(float i_MinutesToCharge)
        {
            r_Vehicle.AddToEnergySource(i_MinutesToCharge);
        }

        internal void SetTireInfo(string[,] i_TireInfo)
        {
            r_Vehicle.SetTireInfo(i_TireInfo);
        }

        internal Dictionary<string, string> GetAllDataForVehicle()
        {
            Dictionary<string, string> VehicleData = r_Vehicle.GetAllDataForVehicle();

            VehicleData["Owner Name"] = OwnerName;
            VehicleData["Owner Phone"] = OwnerPhoneNumber;
            VehicleData["Status"] = m_Status.ToString();

            return VehicleData;
        }

        internal void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            r_Vehicle.SetUniqueMembers(i_FilledUniqueData);
        }

        internal void SetEnergyPercentage(float i_NewEnergyPercentage)
        {
            if (i_NewEnergyPercentage < 0 || i_NewEnergyPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("Energy source Percentage must be in the range 0 - 100.");
            }

            r_Vehicle.EnergySourcePercentage = i_NewEnergyPercentage;
        }

        public enum eVehicleStatuses
        {
            WorkInProgress,
            WorkFinished,
            Paid
        }

    }

}