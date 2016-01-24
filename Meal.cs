using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellyCondonFinalProject
{
    class Meal
    {
        private static int bfQuantity = 10;
        private static double bfPrice = 6.45;
        private static int lQuantity = 20;
        private static double lPrice = 8.95;
        private static int dQuantity = 35;
        private static double dPrice = 9.72;
        public static int breakfastQuantity
        {
            get { return bfQuantity; }
            set { bfQuantity = value; }
        }
        public static double breakfastPrice
        {
            get { return bfPrice; }
            set { bfPrice = value; }
        }
        public static int lunchQuantity
        {
            get { return lQuantity; }
            set { lQuantity = value; }
        }
        public static double lunchPrice
        {
            get { return lPrice; }
            set { lPrice = value; }
        }
        public static int dinnerQuantity
        {
            get { return dQuantity; }
            set { dQuantity = value; }
        }
        public static double dinnerPrice
        {
            get { return dPrice; }
            set { dPrice = value; }
        }

        public Meal()
        {
        }

        public static void ChooseMealType(Customer[] customers)
        {
            int mealSelection = 0;
            int mealQuantity = 0;
            double subTotal = 0;
            bool showMenu = true;
            while (showMenu)
            {
                Menu.displayBanner();
                Console.WriteLine("Make your Meal Selection:\n\t1 - Breakfast (${0} - {3} Remaining)\n\t2 - Lunch (${1} - {4} Remaining)\n\t3 - Dinner (${2} - {5} Remaining)\n\t4 - Exit", breakfastPrice, lunchPrice, dinnerPrice, breakfastQuantity, lunchQuantity, dinnerQuantity);
                mealSelection = Menu.userSelection();
                switch (mealSelection)
                {
                    case 1:
                    case 2:
                    case 3:
                        Console.Write("\nEnter Quantity: ");
                        bool mQuantity = int.TryParse(Console.ReadLine(), out mealQuantity);
                        if (mealQuantity == 0)
                        {
                            Console.WriteLine("Invalid Quantity Entered");
                            Console.WriteLine("\nPress any key to continue . . .");
                            Console.ReadKey();
                        }
                        else if (CheckQuantity(mealSelection, mealQuantity))
                        {
                            subTotal = CalculateTotal(mealSelection, mealQuantity);
                            double orderTotal = subTotal * (1 + 0.056);
                            Console.WriteLine("\nSub total:\t${0:.00}\nTax:\t\t ${1:.00}\nTotal:\t\t${2:.00}", subTotal, subTotal * 0.056, orderTotal);
                            Console.WriteLine("\nPress any key to continue to payment options . . .");
                            Console.ReadKey();
                            Customer.Payment(customers, mealSelection, mealQuantity, orderTotal);
                            showMenu = false;
                        }
                        else
                        {
                            Console.WriteLine("Sorry, there are insufficient meals remaining to fulfill your request.");
                            Console.WriteLine("\nPress any key to continue . . .");
                            Console.ReadKey();
                        }
                        break;
                    case 4:
                        showMenu = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid Menu Selection.");
                        Console.WriteLine("\nPress any key to continue . . .");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void AdjustQuantity(int mealSelection, int mealQuantity)
        {
            switch (mealSelection)
            {
                case 1:
                    breakfastQuantity -= mealQuantity;
                    break;
                case 2:
                    lunchQuantity -= mealQuantity;
                    break;
                case 3:
                    dinnerQuantity -= mealQuantity;
                    break;
            }
        }

        public static bool CheckQuantity(int mealSelection, int mealQuantity)
        {
            switch (mealSelection)
            {
                case 1:
                    if (mealQuantity > breakfastQuantity)
                        return false;
                    else
                        return true;
                case 2:
                    if (mealQuantity > lunchQuantity)
                        return false;
                    else
                        return true;
                case 3:
                    if (mealQuantity > dinnerQuantity)
                        return false;
                    else
                        return true;
                default:
                    return false;
            }
        }
        public static double CalculateTotal(int mealSelection, int mealQuantity)
        {
            switch (mealSelection)
            {
                case 1:
                    return mealQuantity * 6.45;
                case 2:
                    return mealQuantity * 8.95;
                case 3:
                    return mealQuantity * 9.72;
                default:
                    return 0;
            }
        }

    }
}
