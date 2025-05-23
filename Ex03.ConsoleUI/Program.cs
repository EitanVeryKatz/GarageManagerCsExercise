using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                GarageManagerConsoleUI.PrintMenu();
                GarageManagerConsoleUI.GetUserChoice();
                Console.WriteLine();
            }
        }
    }
}
