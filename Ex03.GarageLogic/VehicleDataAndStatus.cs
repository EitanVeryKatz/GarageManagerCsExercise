using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.GarageLogicManager;

namespace Ex03.GarageLogic
{
    public class VehicleDataAndStatus:VehicleCreator
    {
        private readonly Vehicle r_vehicle;

        private e_StatusOfVehicleInGarage m_status = e_StatusOfVehicleInGarage.WorkInProgress;
        public string OwnerName { get; private set; }
        public string OwnerPhoneNumber { get; private set; }
        public string LicenseId
        {
            get
            {
                return r_vehicle.r_LicenseID;
            }
        }
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
                string[] dataMembers = new string[r_vehicle.uniqueDataMembers.Count];
                r_vehicle.uniqueDataMembers.CopyTo(dataMembers);
                return dataMembers;
            }
        }

        public VehicleDataAndStatus(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentFuelAmount = 0)

        {
            r_vehicle = CreateVehicle(i_VehicleType, i_LicenseID, i_ModelName, i_OwnerName, i_OwnerPhone, i_CurrentFuelAmount);
            OwnerName = i_OwnerName;
            OwnerPhoneNumber = i_OwnerPhone;
            if (r_vehicle is IFuelPowered fuelPoweredVehicle)
            {
                fuelPoweredVehicle.Refuel(fuelPoweredVehicle.GetFuelType(), i_CurrentFuelAmount);
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

        public void Recharge(float i_minutesToCharge)
        {
            r_vehicle.Recharge(i_minutesToCharge);
        }

        internal void SetTireInfo(string[,] i_tireInfo)
        {
            r_vehicle.SetTireInfo(i_tireInfo);
        }

        internal Dictionary<string, string> GetAllDataForVehicle()
        {
            return r_vehicle.GetAllDataForVehicle();
        }

        internal void SetUniqueMembers(Dictionary<string, string> i_FilledUniqueData)
        {
            r_vehicle.SetUniqueMembers(i_FilledUniqueData);
        }

        internal void SetEnergyPrecentage(float i_newEnergyPrecentage)
        {
            r_vehicle.EnergySourcePrecentage = i_newEnergyPrecentage;
        }
    }

}
