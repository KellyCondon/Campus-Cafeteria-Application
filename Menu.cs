using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellyCondonFinalProject
{
    class Menu
    {
        static void Main(string[] args)
        {
            Customer[] customers = {new Customer("Guest", "", "GG", "", "", 0, 0, 0, 0, 0), 
                                    new Customer("Scott", "Summers", "SS", "Xavier's School for Gifted Youngsters", "NY", 3.97, 24, 5, 25.00, 1) 
                                   };

            int menuSelect = 0;
            bool appRunning = true;
            while (appRunning)
            {
                Menu.displayBanner();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1 - Order Meal\n\t2 - View Account Information\n\t3 - Exit");
                menuSelect = Menu.userSelection();
                if (menuSelect == 1)
                {
                    Meal.ChooseMealType(customers);
                }
                else if (menuSelect == 2)
                    Customer.DisplayCustInfo(customers);
                else if (menuSelect == 3)
                    appRunning = false;
                else if (menuSelect == 0)
                {
                    Console.WriteLine("\n\tInvalid Menu Selection.");
                    Console.WriteLine("\nPress any key to return to the Menu . . .");
                    Console.ReadKey();
                }
            }//end while loop
            Console.WriteLine("\n\nPress any key to Exit . . .");
            Console.ReadKey();
        }//end Main method

        public static void displayBanner()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t~*~ Welcome to Sun Devil Dining ~*~\t\t\t\t\t");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static int userSelection()
        {
            Console.Write("Enter your selection: ");
            ConsoleKeyInfo inputKey = Console.ReadKey();
            switch (inputKey.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return 2;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return 3;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
