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
        private static GarageLogicManager r_garageLogic = new GarageLogicManager();

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
                Console.WriteLine($"Invalid input: {input}, Try again.");
                GetUserChoice();
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        r_garageLogic.GetVehiclesFromFile();
                        break;
                    case 2:
                        addNewVehicleFlow();
                        break;
                    case 3:
                        printVehiclesByStatus();
                        break;
                    case 4:
                        updateVehicleStatus();
                        break;
                    case 5:
                        inflateTires();
                        break;
                    case 6:
                        refuelVehicle();
                        break;
                    case 7:
                        rechargeVehicle();
                        break;
                    case 8:
                        showVehicleDetails();
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void addNewVehicleFlow()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseID = Console.ReadLine();

            if (r_garageLogic.IsVehicleInGarage(licenseID))
            {
                r_garageLogic.ChangeVehicleStatus(licenseID, GarageLogicManager.eVehicleStatuses.WorkInProgress);
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

                float currentFuelAmount = getValidatedFloatInput("Please enter CurrentFuelAmount (or 0 if not applicable):");

                if (!isValidInput(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentFuelAmount))
                {
                    Console.WriteLine("Invalid input. Vehicle not added.");
                    return;
                }

                r_garageLogic.AddNewVehicle(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentFuelAmount);

                string[,] wheelData = collectWheelData(licenseID);
                r_garageLogic.UpdateTireInfoForNewVehicle(licenseID, wheelData);

                Dictionary<string, string> uniqueData = collectUniqueData(licenseID);
                r_garageLogic.SetUniqueMembers(licenseID, uniqueData);

                Console.WriteLine("Vehicle added to garage.");
            }
        }

        private static float getValidatedFloatInput(string prompt)
        {
            float value;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (float.TryParse(input, out value) && value >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a non-negative number.");
            }
            return value;
        }

        private static string[,] collectWheelData(string licenseID)
        {
            Console.WriteLine("Would you like to input data for all wheels simultaniously?(y/n)");
            bool getAllWheelDataAtOnce = Console.ReadLine().ToLower() == "y";
            int numOfTires = r_garageLogic.GetAmountOfTires(licenseID);
            string[,] wheelData = new string[numOfTires, 2];

            if (getAllWheelDataAtOnce)
            {
                Console.WriteLine("Please enter tire manufacturer name:");
                string wheelManufacturer = Console.ReadLine();
                string airPressure = getValidatedFloatInput("Please enter tire current Air pressure:").ToString();

                for (int i = 0; i < numOfTires; i++)
                {
                    wheelData[i, 0] = wheelManufacturer;
                    wheelData[i, 1] = airPressure;
                }
            }
            else
            {
                for (int i = 0; i < numOfTires; i++)
                {
                    Console.WriteLine($"Please enter #{i} tire manufacturer name:");
                    wheelData[i, 0] = Console.ReadLine();
                    wheelData[i, 1] = getValidatedFloatInput($"Please enter #{i} tire current Air pressure:").ToString();
                }
            }
            return wheelData;
        }

        private static Dictionary<string, string> collectUniqueData(string licenseID)
        {
            string[] uniqueDataOfNewVehicle = r_garageLogic.GetUniqueDataMembersOfVehicle(licenseID);
            Dictionary<string, string> filledUniqueData = new Dictionary<string, string>();
            foreach (string dataMember in uniqueDataOfNewVehicle)
            {
                Console.WriteLine($"Please Enter {dataMember}:");
                filledUniqueData[dataMember] = Console.ReadLine();
            }
            return filledUniqueData;
        }

        private static void printVehiclesByStatus()
        {
            Console.WriteLine("Please enter status to filter by (All/WorkInProgress/WorkFinished/Paid):");
            string statusInput = Console.ReadLine();

            if (statusInput == "All")
            {
                Console.WriteLine("Showing all vehicles.");
                List<string> vehicles = r_garageLogic.GetAllLicanseNumbersOfVehiclesInGarage();
                foreach (string vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                }
            }
            else
            {
                List<string> vehicles;
                try
                {
                    vehicles = r_garageLogic.GetAllLicanseNumbersOfVehiclesInGarage(statusInput);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Invalid status: {statusInput}");
                    return;
                }
                Console.WriteLine($"Vehicles in garage with status {statusInput}:");
                foreach (string vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                }
            }
        }

        private static void updateVehicleStatus()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseId = Console.ReadLine();
            Console.WriteLine("Please enter new status (WorkInProgress/WorkFinished/Paid):");
            string statusInputStr = Console.ReadLine();
            GarageLogicManager.eVehicleStatuses o_newStatus;
            if (!Enum.TryParse(statusInputStr, true, out o_newStatus))
            {
                Console.WriteLine("Invalid status. Please try again.");
            }
            else
            {
                r_garageLogic.ChangeVehicleStatus(licenseId, o_newStatus);
                Console.WriteLine("Vehicle status updated.");
            }
        }

        private static void inflateTires()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToInflate = Console.ReadLine();
            if (r_garageLogic.IsVehicleInGarage(licenseIdToInflate))
            {
                r_garageLogic.FillAirInVehicle(licenseIdToInflate);
                Console.WriteLine("Tires inflated.");
            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }
        }

        private static void refuelVehicle()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToRefuel = Console.ReadLine();
            float amountToRefuel = getValidatedFloatInput("Please enter amount to refuel:");
            Console.WriteLine("Please enter fuel type: Octan98/Octan96/Octan95/Soler");
            string fuelChoice = Console.ReadLine();
            if (r_garageLogic.IsVehicleInGarage(licenseIdToRefuel))
            {
                try
                {
                    r_garageLogic.RefuelVehicle(licenseIdToRefuel, fuelChoice, amountToRefuel);
                    Console.WriteLine("Vehicle refueled.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Invalid fuel type: {fuelChoice}");
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }
        }

        private static void rechargeVehicle()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToRecharge = Console.ReadLine();
            float minutesToRecharge = getValidatedFloatInput("Please enter amount to recharge:");
            if (r_garageLogic.IsVehicleInGarage(licenseIdToRecharge))
            {
                r_garageLogic.RechargeVehicle(licenseIdToRecharge, minutesToRecharge);
                Console.WriteLine("Vehicle recharged.");
            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }
        }

        private static void showVehicleDetails()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToShow = Console.ReadLine();
            if (r_garageLogic.IsVehicleInGarage(licenseIdToShow))
            {
                foreach (KeyValuePair<string, string> pair in r_garageLogic.GetAllDataForVehicle(licenseIdToShow))
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
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
            if (string.IsNullOrEmpty(ownerPhone) || !long.TryParse(ownerPhone, out _) || ownerPhone.Length < 7)
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
