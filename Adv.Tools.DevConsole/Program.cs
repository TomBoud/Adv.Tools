using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DevConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ADV Tools Dev Console, please select a routine");
                Console.Write(Environment.NewLine);
                Console.WriteLine("1. Run Main Model Quality View UI");
                Console.WriteLine("2. Exit");
                Console.WriteLine("3. Exit");
                Console.Write(Environment.NewLine);
                Console.Write("Enter your choice: ");

                int choice = GetMenuChoice(1, 3);

                switch (choice)
                {
                    case 1:
                        new RunMainModelQualityView();
                        break;
                    case 2:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    case 3:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                }
            }
        }

        static int GetMenuChoice(int minValue, int maxValue)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= minValue && choice <= maxValue)
                    {
                        return choice;
                    }
                }
                Console.Write("Invalid input. Please enter a valid choice: ");
            }
        }
    }
}
