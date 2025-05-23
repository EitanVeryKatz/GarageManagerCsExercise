using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class GarageManagerConsoleUI
    {
        public static void PrintMenu()
        {
            Console.WriteLine("===== Garage Menu =====");
            Console.WriteLine("1. Load vehicles from Vehicles.db");
            Console.WriteLine("2. Add new vehicle");
            Console.WriteLine("3. Show all vehicles");
            Console.WriteLine("4. Update vehicle status");
            Console.WriteLine("5. Inflate vehicle tires");
            Console.WriteLine("6. Refuel/Recharge vehicle");
            Console.WriteLine("7. Show vehicle details");
            Console.WriteLine("8. Exit");
            Console.Write("Select an option: ");
        }

        public static void GetUserChoice()
        {
            int choice = -1;
            string input = Console.ReadLine();
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 8)
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
                        // Add new vehicle
                        break;
                    case 3:
                        // Show all vehicles
                        break;
                    case 4:
                        // Update vehicle status
                        break;
                    case 5:
                        // Inflate tires
                        break;
                    case 6:
                        // Refuel/Recharge vehicle
                        break;
                    case 7:
                        // Show vehicle details
                        break;
                    case 8:
                        // Exit
                        Environment.Exit(0);
                        break;
                }
            }
        }


    }
}
