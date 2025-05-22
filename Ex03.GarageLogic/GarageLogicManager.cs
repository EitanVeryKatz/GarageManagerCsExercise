using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        public readonly Dictionary<Vehicle, VehicleDataAndStatus> m_vehicles = new Dictionary<Vehicle, VehicleDataAndStatus>();

        public class VehicleDataAndStatus
        {
            private e_StatusOfVehicleInGarage m_status = e_StatusOfVehicleInGarage.WorkInProgress;
            public string OwnerName { get; private set; }
            public string OwnerPhoneNumber { get; private set; }

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
            
        }

        public enum e_StatusOfVehicleInGarage
        {
            WorkInProgress,
            WorkFinished,
            Paid
        }
    }
}
