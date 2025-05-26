using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                string[] dataMembers = new string[r_vehicle.UniqueDataMembers.Count];
                r_vehicle.UniqueDataMembers.CopyTo(dataMembers);

                return dataMembers;
            }

        }

        public VehicleDataAndStatus(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentFuelAmount = 0)
        {
            r_vehicle = CreateVehicle(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone, i_CurrentFuelAmount);
            OwnerName = i_OwnerName;
            OwnerPhoneNumber = i_OwnerPhone;
            if(i_CurrentFuelAmount > 0) {
                Refuel(r_vehicle.GetFuelType(), i_CurrentFuelAmount);
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

        internal void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            r_vehicle.SetUniqueMembers(i_FilledUniqueData);
        }

        internal void SetEnergyPrecentage(float i_newEnergyPrecentage)
        {
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