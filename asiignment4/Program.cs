using asiignment4.Model;

namespace asiignment4 {
    internal class Program {
        static void Main(string[] args) {

            #region Question 1
            /** 
             1. Develop a C# program where you need to track the total number of users
                who have logged in. The number should persist even if different users log
                in.(use static Field and achieve the functionality)
             */

            Console.WriteLine("\n\n-------------- Question 1 ------------\n\n");

            UserLoginTracker user1 = new UserLoginTracker("Omkar");
            UserLoginTracker user2 = new UserLoginTracker("Akshay");
            UserLoginTracker user3 = new UserLoginTracker("Prathmesh");

            Console.WriteLine($"\nTotal Users Login:: {UserLoginTracker.GetTotalUsersLoggedIn()}");

            #endregion

            #region Question 2
            /** 
             * 2. A company has an Employee class with properties Name and Salary.
                     The company also has a Manager class that inherits from Employee
                    and has an additional property Bonus.
                     The Employee class has a method DisplayDetails(),
                     which prints the employee’s name and salary.
                     The Manager class overrides this method to include the bonus
                    amount in the output
             */

            Console.WriteLine("\n\n-------------- Question 2 ------------\n\n");

            Employee emp = new Employee("Omkar", 50000);
            Manager mgr = new Manager("Prashant", 70000, 5000);

            emp.DisplayDetails();
            mgr.DisplayDetails();

            #endregion
        }
    }
}
