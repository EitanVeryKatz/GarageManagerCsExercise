namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GarageManagerConsoleUI garageManagerConsoleUI = new GarageManagerConsoleUI();

            while (garageManagerConsoleUI.Running)
            {
                garageManagerConsoleUI.PrintMenu();
                garageManagerConsoleUI.GetUserChoice();
            }

        }

    }

}
