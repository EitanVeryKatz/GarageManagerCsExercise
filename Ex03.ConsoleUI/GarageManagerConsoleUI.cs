using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class GarageManagerConsoleUI
    {
        private GarageLogicManager r_garageLogic = new GarageLogicManager();
        private readonly string[] r_validStatusArr = {"WorkInProgress","WorkFinished","Paid"};
        public bool Running { get; private set; }

        public GarageManagerConsoleUI()
        {
            Running = true;
        }

        public void PrintMenu()
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

        public void GetUserChoice()
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out int o_optionChoise) == false || o_optionChoise < 1 || o_optionChoise > 9)
            {
                Console.WriteLine($"Invalid input: {input}, Try again.");
                GetUserChoice();
            }
            else
            {
                switch (o_optionChoise)
                {
                    case 1:
                        r_garageLogic.GetVehiclesFromFile();
                        break;
                    case 2:
                        addNewVehicle();
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
                        Running = false;
                        break;
                }

            }

            Console.WriteLine();
        }

        private void addNewVehicle()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseID = Console.ReadLine();

            if (r_garageLogic.IsVehicleInGarage(licenseID))
            {
                r_garageLogic.ChangeVehicleStatus(licenseID);
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
                float currentEnergySourcePrecentage = getValidatedFloatInput("Please enter Current Fuel/Battery Precentage");

                if (!isValidInput(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentEnergySourcePrecentage))
                {
                    Console.WriteLine("Invalid input. Vehicle not added.");
                }
                else
                {
                    try
                    {
                        r_garageLogic.AddNewVehicle(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentEnergySourcePrecentage);
                        string[,] wheelData = collectWheelData(licenseID);
                        r_garageLogic.UpdateTireInfoForNewVehicle(licenseID, wheelData);

                        Dictionary<string, string> uniqueData = collectUniqueData(licenseID);
                        r_garageLogic.SetUniqueMembers(licenseID, uniqueData);

                        Console.WriteLine("Vehicle added to garage.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Vehicle not added.");
                    }

                }

            }

        }

        private float getValidatedFloatInput(string i_prompt)
        {
            float o_value;
            while (true)
            {
                Console.WriteLine(i_prompt);
                string input = Console.ReadLine();

                if (float.TryParse(input, out o_value) && o_value >= 0)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a non-negative number.");
            }

            return o_value;
        }

        private string[,] collectWheelData(string licenseID)
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

        private  Dictionary<string, string> collectUniqueData(string licenseID)
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

        private void printVehiclesByStatus()
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
                if (isValidStatus(statusInput))
                {
                    List<string> vehicles = r_garageLogic.GetAllLicanseNumbersOfVehiclesInGarage(statusInput);

                    Console.WriteLine($"Vehicles in garage with status {statusInput}:");
                    foreach (string vehicle in vehicles)
                    {
                        Console.WriteLine(vehicle);
                    }

                }
                else
                {
                    Console.WriteLine($"Invalid status: {statusInput}");
                }

            }

        }

        private void updateVehicleStatus()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseId = Console.ReadLine();

            Console.WriteLine("Please enter new status (WorkInProgress/WorkFinished/Paid):");
            string statusInputStr = Console.ReadLine();

            if (isValidStatus(statusInputStr))
            {
                try
                {
                    r_garageLogic.ChangeVehicleStatus(licenseId, statusInputStr);
                    Console.WriteLine("Vehicle status updated.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Status not  currently supported");
                }

            }
            else
            {
                Console.WriteLine("Invalid status. Please try again.");
            }

        }

        private void inflateTires()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseId = Console.ReadLine();

            if (r_garageLogic.IsVehicleInGarage(licenseId))
            {
                r_garageLogic.FillAirInVehicle(licenseId);
                Console.WriteLine("Tires inflated.");
            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }

        }

        private void refuelVehicle()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseId = Console.ReadLine();
            float amountToRefuel = getValidatedFloatInput("Please enter amount to refuel:");

            Console.WriteLine("Please enter fuel type: Octan98/Octan96/Octan95/Soler");
            string fuelChoice = Console.ReadLine();

            if (r_garageLogic.IsVehicleInGarage(licenseId))
            {
                try
                {
                    r_garageLogic.RefuelVehicle(licenseId, fuelChoice, amountToRefuel);
                    Console.WriteLine("Vehicle refueled.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Invalid fuel type: {fuelChoice}");
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }

        }

        private void rechargeVehicle()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToRecharge = Console.ReadLine();
            float minutesToRecharge = getValidatedFloatInput("Please enter amount to recharge:");

            if (r_garageLogic.IsVehicleInGarage(licenseIdToRecharge))
            {
                try
                {
                    r_garageLogic.RechargeVehicle(licenseIdToRecharge, minutesToRecharge);
                    Console.WriteLine("Vehicle recharged.");
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }catch(ArgumentException)
                {
                    Console.WriteLine("Cannot recarge fuel powered vehicle.");
                }

            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }
        }

        private  void showVehicleDetails()
        {
            Console.WriteLine("Please enter LicenseID:");
            string licenseIdToShow = Console.ReadLine();
            if (r_garageLogic.IsVehicleInGarage(licenseIdToShow))
            {
                foreach (KeyValuePair<string, string> dataMember in r_garageLogic.GetAllDataForVehicle(licenseIdToShow))
                {
                    Console.WriteLine($"{dataMember.Key}: {dataMember.Value}");
                }

            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }

        }

        private bool isValidInput(string vehicleType, string licenseID, string modelName, string ownerName, string ownerPhone, float currentFuelAmount)
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

            if (string.IsNullOrEmpty(ownerPhone) || isValidPhone(ownerPhone) == false)
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

        private bool isValidPhone(string i_phone)
        {
            bool isValid = true;

            if (i_phone.Length < 10 || i_phone.Length > 11)
            {
                isValid = false;
            }
            else
            {
                string firstPartOfPhone = i_phone.Substring(0, 3);
                string secondPartOfPhone = i_phone.Substring(4);

                isValid = int.TryParse(firstPartOfPhone, out int o_result) && int.TryParse(secondPartOfPhone, out o_result) && i_phone[3] == '-';
            }

            return isValid;
        }

        private bool isValidStatus(string i_status)
        {
            return r_validStatusArr.Contains(i_status);
        }

    }

}
