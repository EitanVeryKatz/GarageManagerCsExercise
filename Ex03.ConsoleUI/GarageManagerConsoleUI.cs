using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    internal class GarageManagerConsoleUI
    {
        private static GarageLogicManager GarageLogic = new GarageLogicManager();

        public static void PrintMenu()
        {
            Console.WriteLine("===== Garage Menu =====");
            Console.WriteLine("1. Load vehicles from Vehicles.db");
            Console.WriteLine("2. Add new vehicle");
            Console.WriteLine("3. Show all vehicles");
            Console.WriteLine("4. Update vehicle status");
            Console.WriteLine("5. Inflate vehicle tires");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Recharge vehicle");
            Console.WriteLine("8. Show vehicle details");
            Console.WriteLine("9. Exit");
            Console.Write("Select an option: ");
        }

        public static void GetUserChoice()
        {
            int choice = -1;
            string input = Console.ReadLine();
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 9)
            {
                Console.WriteLine("Illegal option, Try again.");
                GetUserChoice();
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        // Load vehicles from file
                        break;
                    case 2:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseID = Console.ReadLine();

                        if (GarageLogic.IsVehicleInGarage(licenseID))
                        {
                            GarageLogic.ChangeVehicleStatus(licenseID, GarageLogicManager.e_StatusOfVehicleInGarage.WorkInProgress);
                            Console.WriteLine("Vehicle already in garage. Status updated to Work In Progress.");
                        }
                        else
                        {
                            Console.WriteLine("Please enter ModelName:");
                            string modelName = Console.ReadLine();
                            Console.WriteLine("Please enter OwnerName:");
                            string ownerName = Console.ReadLine();
                            Console.WriteLine("Please enter OwnerPhone:");
                            string ownerPhone = Console.ReadLine();
                            Console.WriteLine("Please enter VehicleType:");
                            string vehicleType = Console.ReadLine();
                            Console.WriteLine("Please enter CurrentFuelAmount (or 0 if not applicable):");
                            float currentFuelAmount = 0;
                            float.TryParse(Console.ReadLine(), out currentFuelAmount);
                            bool validInput = isValidInput(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentFuelAmount);
                            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentFuelAmount);
                            GarageLogic.AddNewVehicle(vehicle);
                            Console.WriteLine("Vehicle added to garage.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Please enter status to filter by (All/WorkInProgress/WorkFinished/Paid):");
                        string statusInput = Console.ReadLine();
                        GarageLogicManager.e_StatusOfVehicleInGarage status;
                        if (Enum.TryParse(statusInput, true, out status))
                        {
                            List<string> vehicles = GarageLogic.GetAllLicanseNumbersOfVehiclesInGarage(status);
                            Console.WriteLine("Vehicles in garage with status " + status + ":");
                            foreach (string vehicle in vehicles)
                            {
                                Console.WriteLine(vehicle);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Showing all vehicles.");
                            List<string> vehicles = GarageLogic.GetAllLicanseNumbersOfVehiclesInGarage();
                            foreach (string vehicle in vehicles)
                            {
                                Console.WriteLine(vehicle);
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseId = Console.ReadLine();
                        Console.WriteLine("Please enter new status (WorkInProgress/WorkFinished/Paid):");
                        string statusInputStr = Console.ReadLine();
                        GarageLogicManager.e_StatusOfVehicleInGarage o_newStatus;

                        if (Enum.TryParse(statusInputStr, true, out o_newStatus) == false)
                        {
                            Console.WriteLine("Invalid status. Please try again.");
                        }
                        else
                        {
                            GarageLogic.ChangeVehicleStatus(licenseId, o_newStatus);
                            Console.WriteLine("Vehicle status updated.");
                        }

                        break;
                    case 5:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseIdToInflate = Console.ReadLine();
                        if (GarageLogic.IsVehicleInGarage(licenseIdToInflate))
                        {
                            GarageLogic.FillAirInVehicle(licenseIdToInflate);
                            Console.WriteLine("Tires inflated.");
                        }
                        else
                        {
                            Console.WriteLine("Vehicle not found in garage.");
                        }

                        break;
                    case 6:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseIdToRefuel = Console.ReadLine();
                        Console.WriteLine("Please enter amount to refuel:");
                        float amountToRefuel = 0;
                        float.TryParse(Console.ReadLine(), out amountToRefuel);
                        if (GarageLogic.IsVehicleInGarage(licenseIdToRefuel))
                        {
                            GarageLogic.RefuelVehicle(licenseIdToRefuel, amountToRefuel);
                            Console.WriteLine("Vehicle refueled.");
                        }
                        else
                        {
                            Console.WriteLine("Vehicle not found in garage.");
                        }
                        break;
                    case 7:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseIdToRecharge = Console.ReadLine();
                        Console.WriteLine("Please enter amount to recharge:");
                        float minutesToRecharge = 0;
                        float.TryParse(Console.ReadLine(), out minutesToRecharge);
                        if (GarageLogic.IsVehicleInGarage(licenseIdToRecharge))
                        {
                            GarageLogic.RechargeVehicle(licenseIdToRecharge, minutesToRecharge);
                            Console.WriteLine("Vehicle recharged.");
                        }
                        else
                        {
                            Console.WriteLine("Vehicle not found in garage.");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Please enter LicenseID:");
                        string licenseIdToShow = Console.ReadLine();
                        if (GarageLogic.IsVehicleInGarage(licenseIdToShow))
                        {
                            Vehicle vehicle = GarageLogic.GetVehicleByLicenseID(licenseIdToShow);
                            Console.WriteLine(vehicle.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Vehicle not found in garage.");
                        }
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        private static bool isValidInput(string vehicleType, string licenseID, string modelName, string ownerName, string ownerPhone, float currentFuelAmount)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(vehicleType))
            {
                Console.WriteLine("Invalid vehicle type.");
                isValid = false;
            }
            if (string.IsNullOrEmpty(licenseID))
            {
                Console.WriteLine("Invalid license ID.");
                isValid = false;
            }
            if (string.IsNullOrEmpty(modelName))
            {
                Console.WriteLine("Invalid model name.");
                isValid = false;
            }
            if (string.IsNullOrEmpty(ownerName))
            {
                Console.WriteLine("Invalid owner name.");
                isValid = false;
            }
            if (string.IsNullOrEmpty(ownerPhone) || int.TryParse(ownerPhone, out int phone))
            {
                Console.WriteLine("Invalid owner phone.");
                isValid = false;
            }
            if (currentFuelAmount < 0)
            {
                Console.WriteLine("Invalid fuel amount.");
                isValid = false;
            }

            return isValid;
        }
    }

}
