using assignment_6.Model;

namespace assignment_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question 1
            /**
             * A bank has a token system where customers take a token and are served in the order they arrived. You need to: 
             * Add new customers to the queue. Serve customers in a First In, First Out (FIFO) order.
             * Check who is next in line without removing them. 
             */

            Console.WriteLine("\n\n-------------- Question 1 ------------\n\n");

            BankQueue bankQueue = new BankQueue();

            bankQueue.AddCustomer(); 
            bankQueue.AddCustomer(); 
            bankQueue.CheckNextCustomer(); 
            bankQueue.ServeCustomer(); 
            bankQueue.CheckNextCustomer(); 
            bankQueue.ServeCustomer(); 
            bankQueue.ServeCustomer();

            #endregion

            #region Question 2
            /**
             * A university is conducting an event where students register for workshops. However, a student can only register once for a particular workshop.
                You need to store unique student IDs for each workshop.
                If a student tries to register again, the system should prevent duplicates. Implement with C#
             */

            Console.WriteLine("\n\n-------------- Question 2 ------------\n\n");

            UniversityEvent eventSystem = new UniversityEvent();

            eventSystem.RegisterStudent("AI Workshop", 101);
            eventSystem.RegisterStudent("AI Workshop", 102);
            eventSystem.RegisterStudent("AI Workshop", 101); // Duplicate registration
            eventSystem.RegisterStudent("Blockchain Workshop", 103);

            eventSystem.DisplayRegistrations("AI Workshop");
            eventSystem.DisplayRegistrations("Blockchain Workshop");

            #endregion

        }
    }
}
