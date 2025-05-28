using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class GarageManagerConsoleUI
    {
        private GarageLogicManager r_GarageLogic = new GarageLogicManager();
        private readonly string[] r_ValidStatusArr = {"WorkInProgress","WorkFinished","Paid"};
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

            if (int.TryParse(input, out int o_OptionChoise) == false || o_OptionChoise < 1 || o_OptionChoise > 9)
            {
                Console.WriteLine($"Invalid input: {input}, Try again.");
                GetUserChoice();
            }
            else
            {
                switch (o_OptionChoise)
                {
                    case 1:
                        r_GarageLogic.GetVehiclesFromFile();
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

            if (r_GarageLogic.IsVehicleInGarage(licenseID))
            {
                r_GarageLogic.ChangeVehicleStatus(licenseID);
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
                float currentEnergySourcePercentage = getValidatedFloatInput("Please enter Current Fuel/Battery Percentage");

                if (!isValidInput(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentEnergySourcePercentage))
                {
                    Console.WriteLine("Invalid input. Vehicle not added.");
                }
                else
                {
                    try
                    {
                        r_GarageLogic.AddNewVehicle(vehicleType, licenseID, modelName, ownerName, ownerPhone, currentEnergySourcePercentage);
                        string[,] wheelData = collectWheelData(licenseID);
                        r_GarageLogic.UpdateTireInfoForNewVehicle(licenseID, wheelData);

                        Dictionary<string, string> uniqueData = collectUniqueData(licenseID);
                        r_GarageLogic.SetUniqueMembers(licenseID, uniqueData);

                        Console.WriteLine("Vehicle added to garage.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Vehicle not added.");
                    }

                }

            }

        }

        private float getValidatedFloatInput(string i_Prompt)
        {
            float o_Value;
            while (true)
            {
                Console.WriteLine(i_Prompt);
                string input = Console.ReadLine();

                if (float.TryParse(input, out o_Value) && o_Value >= 0)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a non-negative number.");
            }

            return o_Value;
        }

        private string[,] collectWheelData(string i_LicenseId)
        {
            Console.WriteLine("Would you like to input data for all wheels simultaniously?(y/n)");
            bool getAllWheelDataAtOnce = Console.ReadLine().ToLower() == "y";
            int numOfTires = r_GarageLogic.GetAmountOfTires(i_LicenseId);
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

        private  Dictionary<string, string> collectUniqueData(string i_LicenseID)
        {
            string[] uniqueDataOfNewVehicle = r_GarageLogic.GetUniqueDataMembersOfVehicle(i_LicenseID);
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
                List<string> vehicles = r_GarageLogic.GetAllLicenseIDsOfVehicles();

                foreach (string vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                }

            }
            else
            {
                if (isValidStatus(statusInput))
                {
                    List<string> vehicles = r_GarageLogic.GetLicenseIDsOfVehiclesByStatus(statusInput);

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
                    r_GarageLogic.ChangeVehicleStatus(licenseId, statusInputStr);
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

            if (r_GarageLogic.IsVehicleInGarage(licenseId))
            {
                r_GarageLogic.FillAirInVehicle(licenseId);
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

            if (r_GarageLogic.IsVehicleInGarage(licenseId))
            {
                try
                {
                    r_GarageLogic.RefuelVehicle(licenseId, fuelChoice, amountToRefuel);
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

            if (r_GarageLogic.IsVehicleInGarage(licenseIdToRecharge))
            {
                try
                {
                    r_GarageLogic.RechargeVehicle(licenseIdToRecharge, minutesToRecharge);
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
            if (r_GarageLogic.IsVehicleInGarage(licenseIdToShow))
            {
                foreach (KeyValuePair<string, string> dataMember in r_GarageLogic.GetAllDataForVehicle(licenseIdToShow))
                {
                    Console.WriteLine($"{dataMember.Key}: {dataMember.Value}");
                }

            }
            else
            {
                Console.WriteLine("Vehicle not found in garage.");
            }

        }

        private bool isValidInput(string i_VehicleType, string i_LicenseID, string i_ModelName, string i_OwnerName, string i_OwnerPhone, float i_CurrentFuelAmount)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(i_VehicleType))
            {
                Console.WriteLine("Invalid vehicle type.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(i_LicenseID))
            {
                Console.WriteLine("Invalid license ID.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(i_ModelName))
            {
                Console.WriteLine("Invalid model name.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(i_OwnerName))
            {
                Console.WriteLine("Invalid owner name.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(i_OwnerPhone) || isValidPhone(i_OwnerPhone) == false)
            {
                Console.WriteLine("Invalid owner phone.");
                isValid = false;
            }

            if (i_CurrentFuelAmount < 0)
            {
                Console.WriteLine("Invalid fuel amount.");
                isValid = false;
            }

            return isValid;
        }

        private bool isValidPhone(string i_Phone)
        {
            bool isValid = true;

            if (i_Phone.Length < 10 || i_Phone.Length > 11)
            {
                isValid = false;
            }
            else
            {
                string firstPartOfPhone = i_Phone.Substring(0, 3);
                string secondPartOfPhone = i_Phone.Substring(4);

                isValid = int.TryParse(firstPartOfPhone, out int o_Result) && int.TryParse(secondPartOfPhone, out o_Result) && i_Phone[3] == '-';
            }

            return isValid;
        }

        private bool isValidStatus(string i_Status)
        {
            return r_ValidStatusArr.Contains(i_Status);
        }

    }

}
