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
            Console.WriteLine("1. טעינת רכבים מקובץ Vehicles.db");
            Console.WriteLine("2. הוסף רכב חדש");
            Console.WriteLine("3. הצג את כל הרכבים");
            Console.WriteLine("4. עדכן סטטוס רכב");
            Console.WriteLine("5. ניפוח גלגלים לרכב");
            Console.WriteLine("6. תדלוק/טעינת רכב");
            Console.WriteLine("7. הצג פרטי רכב");
            Console.WriteLine("8. יציאה");
            Console.Write("בחר אופציה: ");
        }
        public static void GetUserChoice()
        {
            int choice = -1;
            string input = Console.ReadLine();
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 8)
            {
                Console.WriteLine("אופציה לא חוקית, נסה שוב.");
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
