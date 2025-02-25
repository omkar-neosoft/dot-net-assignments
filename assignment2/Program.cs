namespace assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question1

            /** 
             * 1. A software company wants to develop a Salary Calculation System that
                    calculates the Net Salary of employees based on their basic salary, tax
                    deductions, and bonus. The system should perform various operations
                    using arithmetic, relational, logical operators
                     Take the basic salary as user input.
                     Calculate a tax deduction (10% of the basic salary).
                     Add a bonus based on performance ratings:
                         Rating >= 8 → Bonus = 20% of the basic salary.
                         Rating >= 5 and < 8 → Bonus = 10% of the basic salary.
                         Rating < 5 → No bonus.
                     Display the computed salary after all calculations. 
             
             */

            Console.WriteLine("\n\n------------------- Question 1 -----------\n\n");
            // Taking user input for basic salary
            Console.Write("Enter the basic salary: ");
            double basicSalary = Convert.ToDouble(Console.ReadLine());

            // Calculate tax deduction (10% of basic salary)
            double taxDeduction = 0.10 * basicSalary;

            // Taking user input for performance rating
            Console.Write("Enter performance rating (0-10): ");
            int rating = Convert.ToInt32(Console.ReadLine());

            // Calculate bonus based on performance rating
            double bonus = 0;

            if (rating >= 8)
            {
                bonus = 0.20 * basicSalary; // 20% bonus
            }
            else if (rating >= 5)
            {
                bonus = 0.10 * basicSalary; // 10% bonus
            }
            // If rating < 5, no bonus, so bonus remains 0

            // Calculate net salary
            double netSalary = basicSalary - taxDeduction + bonus;

            // Display the computed salary after calculations
            Console.WriteLine("\nSalary Calculation Summary:");
            Console.WriteLine($"Basic Salary: {basicSalary}");
            Console.WriteLine($"Tax Deduction (10%): {taxDeduction}");
            Console.WriteLine($"Bonus: {bonus}");
            Console.WriteLine($"Net Salary: {netSalary}");

            #endregion

            #region Question 2

            /** 
             * 2. Create a program that Facilitates online train ticket booking and
                    calculates the total cost. Display ticket types (General-200,Ac-1000rs,
                    sleeper-500), decide the price as per your wish,ask the user to input the
                    type and number of tickets they want and calculate the total cost.
                     User can book multiple tickets until he types exit(use appropriate
                    loop and statements). 
             */

            Console.WriteLine("\n\n------------------- Question 2 -----------\n\n");

            // Define ticket prices
            double generalTicketPrice = 200;
            double acTicketPrice = 1000;
            double sleeperTicketPrice = 500;

            while (true)
            {
                Console.WriteLine("Available ticket types: ");
                Console.WriteLine("1. General - Rs. 200");
                Console.WriteLine("2. AC - Rs. 1000");
                Console.WriteLine("3. Sleeper - Rs. 500");

                Console.Write("Please enter the type of ticket you want to book (1/2/3): ");
                string ticketTypeInput = Console.ReadLine();
                int ticketType = 0;

                if (!int.TryParse(ticketTypeInput, out ticketType) || ticketType < 1 || ticketType > 3)
                {
                    Console.WriteLine("Invalid input! Please enter a valid ticket type (1, 2, or 3).\n\n");
                    continue;
                }

                Console.Write("\nEnter the number of tickets you want to book: ");
                int numberOfTickets;
                if (!int.TryParse(Console.ReadLine(), out numberOfTickets) || numberOfTickets <= 0)
                {
                    Console.WriteLine("Invalid number of tickets! Please enter a positive number.\n\n");
                    continue;
                }

                double totalCost = 0;
                if (ticketType == 1)
                {
                    totalCost = generalTicketPrice * numberOfTickets;
                }
                else if (ticketType == 2)
                {
                    totalCost = acTicketPrice * numberOfTickets;
                }
                else if (ticketType == 3)
                {
                    totalCost = sleeperTicketPrice * numberOfTickets;
                }

                string ticketTypeName = ticketType == 1 ? "General" : (ticketType == 2 ? "AC" : "Sleeper");
                Console.WriteLine($"\nYou have selected {ticketTypeName} tickets.");
                Console.WriteLine($"Number of tickets: {numberOfTickets}");
                Console.WriteLine($"Total cost: Rs. {totalCost}");

                Console.Write("\nDo you want to book more tickets? (yes/exit): ");
                string continueBooking = Console.ReadLine().ToLower();

                if (continueBooking == "exit")
                {
                    break;
                }
            }

            Console.WriteLine("\nThank you for using the train ticket booking system. Goodbye!\n");


            #endregion

            #region Question 3
            /** 
             * 3. Write a program that allows users of an online shopping platform to
                    check their wallet balance. The platform stores multiple users with their
                    respective wallet balances.
                     Users can enter their user ID to check their balance
                     If the user enters wrong Id allow him to enter the proper Id (use
                    loops) until he enters correct one.
                     If Id is validated display the wallet balance
             */
            Console.WriteLine("\n\n------------------- Question 3 -----------\n\n");

            int[,] userWallets = {
                { 1, 100 },
                { 2, 500 },
                { 3, 700 },
                { 4, 9000 },
                { 5, 1000 },
            };

            userIdLoop:
                Console.Write("Enter user id: ");

                int userId = Convert.ToInt32(Console.ReadLine());

                bool isUserFind = false;
                for (int i = 0; i < userWallets.GetLength(0); i++)
                {
                    if (userWallets[i, 0] == userId)
                    {
                        isUserFind = true;
                        Console.WriteLine($"Your wallet balance is: {userWallets[i, 1]}");
                    }
                }

                if (!isUserFind)
                {
                    Console.WriteLine("User not found!\n");
                    goto userIdLoop;
                }



            #endregion
        }
    }
}
