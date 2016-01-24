using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellyCondonFinalProject
{
    class Customer
    {
        private string fName;
        private string lName;
        private string cName;
        private string sName;
        private string custInitials;
        private double custGpa;
        private int custCode;
        private int custCredits;
        private double custBalance;
        private int honorCredit;

        public string firstName
        {
            get { return fName; }
            set { fName = value; }
        }
        public string lastName
        {
            get { return lName; }
            set { lName = value; }
        }
        public string college
        {
            get { return cName; }
            set { cName = value; }
        }
        public string state
        {
            get { return sName; }
            set { sName = value; }
        }
        public string initials
        {
            get { return custInitials; }
            set { custInitials = value; }
        }
        public double gpa
        {
            get { return custGpa; }
            set { custGpa = value; }
        }
        public int code
        {
            get { return custCode; }
            set { custCode = value; }
        }
        public int credits
        {
            get { return custCredits; }
            set { custCredits = value; }
        }
        public double balance
        {
            get { return custBalance; }
            set { custBalance = value; }
        }
        public int gpaCredit
        {
            get { return honorCredit; }
            set { honorCredit = value; }
        }
        
        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string initials, string college, string state, double gpa, int code, int credits, double balance, int gpaCredit)
        {
            fName = firstName;
            lName = lastName;
            custInitials = initials;
            cName = college;
            sName = state;
            custGpa = gpa;
            custCode = code;
            custCredits = credits;
            custBalance = balance;
            honorCredit = gpaCredit;
        }

        public static void DisplayCustInfo(Customer[] customers)
        {
            Menu.displayBanner();
            int inputCode = 0;
            string inputInitial = "";
            int studentIndex = 0;
            Console.WriteLine("Please enter your credentials\n");
            inputInitial = Customer.GetCustInfo("Initials");
            bool sCode = int.TryParse(GetCustInfo("Code"), out inputCode);
            studentIndex = ReturnStudent(customers, inputInitial, inputCode);
            Console.Clear();
            Menu.displayBanner();
            Console.WriteLine("{0} {1}\n{2}, {3}\nGPA:\t\t{4}\nBalance:\t${5:.00}\nCredits:\t{6}", customers[studentIndex].firstName, customers[studentIndex].lastName, customers[studentIndex].college, customers[studentIndex].state, customers[studentIndex].gpa, customers[studentIndex].balance, customers[studentIndex].credits);
            Console.WriteLine("\nPress any key to continue . . .");
            Console.Read();
        }

        public static void Payment(Customer[] customers, int mealType, int mealQuantity, double orderCost)
        {
            int userSelect = 0;
            string studentInitials = "";
            int studentCode = 0;
            int studentIndex = 0;
            bool paymentComplete = false;
            bool useHonorCredit = true;

            Console.Clear();
            Menu.displayBanner();
            Console.WriteLine("Please enter your credentials (leave blank to check out as a guest)\n");
            studentInitials = GetCustInfo("Initials");
            bool sCode = int.TryParse(GetCustInfo("Code"), out studentCode);
            studentIndex = ReturnStudent(customers, studentInitials, studentCode);

            while (paymentComplete == false)
            {
                Console.Clear();
                Menu.displayBanner();
                Console.WriteLine("Hello, {0}!", customers[studentIndex].firstName);
                if (customers[studentIndex].gpaCredit > 0 && customers[studentIndex].gpa >= 3.5 && mealQuantity == 1 && useHonorCredit == true)
                {
                    Console.WriteLine("You have 1 free meal credit. Would you like to use it?\n\t1 - Yes\n\t2 - No");
                    userSelect = Menu.userSelection();
                    if (userSelect == 1)
                    {
                        customers[studentIndex].gpaCredit -= 1;
                        Meal.AdjustQuantity(mealType, mealQuantity);
                        Console.WriteLine("\n\nSuccessfully used meal credit.");
                        paymentComplete = true;
                        Console.ReadKey();

                    }
                    else if (userSelect == 2)
                    {
                        useHonorCredit = false;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Choose a Payment Option:\n\t1 - Use Meal Plan Credits\n\t2 - Use Card\n\t3 - View Plan Credits and Card Balance\n\t4 - Cancel");
                    userSelect = Menu.userSelection();
                    Console.WriteLine("\n");
                    switch (userSelect)
                    {
                        case 1:
                            if (customers[studentIndex].credits >= mealQuantity)
                            {
                                customers[studentIndex].credits -= mealQuantity;
                                Meal.AdjustQuantity(mealType, mealQuantity);
                                Console.WriteLine("Successfully used {1} Plan Credit(s)\n\tYou have {0} credit(s) remaining.", customers[studentIndex].credits, mealQuantity);
                                paymentComplete = true;
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You have insufficient credits. Please choose another payment option.");
                                Console.WriteLine("\nPress any key to continue . . .");
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            if (customers[studentIndex].balance >= orderCost)
                            {
                                customers[studentIndex].balance -= orderCost;
                                Meal.AdjustQuantity(mealType, mealQuantity);
                                Console.WriteLine("Successfully charged ${0:.00} to your card balance\n\tYour remaining card balance is ${1:.00}", orderCost, customers[studentIndex].balance);
                                paymentComplete = true;
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You have insufficient funds. Please choose another payment option.");
                                Console.WriteLine("\nPress any key to continue . . .");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            Console.WriteLine("Plan Credits: {0}\nCard Balance: ${1}", customers[studentIndex].credits, customers[studentIndex].balance);
                            Console.WriteLine("\nPress any key to continue . . .");
                            Console.ReadKey();
                            break;
                        case 4:
                            paymentComplete = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Menu Selection.");
                            Console.WriteLine("\nPress any key to continue . . .");
                            Console.ReadKey();
                            break;
                    }
                }
                Console.WriteLine("Press any key to continue . . .");
            }
        }

        public static int ReturnStudent(Customer[] customers, string studentInitial, int studentCode)
        {
            int studentIndexNo = 0;
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].custInitials == studentInitial && customers[i].custCode == studentCode)
                {
                    studentIndexNo = i;
                }
            }
            return studentIndexNo;
        }
        public static string GetCustInfo(string infoNeeded)
        {
            switch (infoNeeded)
            {
                case "Initials":
                    Console.Write("Initials (e.g.: KC): ");
                    return Console.ReadLine();
                case "Code":
                    Console.Write("Security Code: ");
                    return Console.ReadLine();
                default:
                    return "";
            }
        }

    }
}
