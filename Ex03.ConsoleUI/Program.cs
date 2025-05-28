namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            GarageManagerConsoleUI  GarageUIManager = new GarageManagerConsoleUI();

            while (GarageUIManager.Running)
            {
                GarageUIManager.PrintMenu();
                GarageUIManager.GetUserChoice();
            }

        }

    }

}
