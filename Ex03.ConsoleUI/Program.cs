namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GarageManagerConsoleUI garageManagerConsoleUI = new GarageManagerConsoleUI();

            while (true)
            {
                garageManagerConsoleUI.PrintMenu();
                garageManagerConsoleUI.GetUserChoice();
            }

        }

    }

}
