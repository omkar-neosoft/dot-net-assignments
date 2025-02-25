using assignment3.Model;

namespace assignment3 {
    internal class Program {
        static void Main(string[] args) {
            #region Question 1
            /** 
             * 1. You are part of a development team at AutoTech Solutions, working on a
                    Car Management Module for a vehicle dealership. You have been
                    assigned the task of creating a Car class that accepts and displays car
                    details.
                     Declare a Car class with appropriate member functions.
                    o CarID (unique identifier)
                    o Brand (e.g., Toyota, Ford)
                    o Model (e.g., Corolla, Mustang)
                    o Year (manufacturing year)
                    o Price (cost of the car)
                     The member function that accepts car details should display the
                    message "Receiving Car Information".
                     The member function to display car details should show the message
                    "Presenting Car Information".            
            */

            Console.WriteLine("\n\n------------------- Question 1 -----------\n\n");

            Car car = new Car();

            car.AcceptCarDetails();

            car.DisplayCarDetails();

            #endregion

            #region Question 2

            /** 
             2. For the above program Implement a parameterized constructor that
                initializes these properties when an object is created.
                 Implement Encapsulation by using private fields with public
                properties.
                 Overload the constructor to allow object creation with
                default values
             */

            Console.WriteLine("\n\n------------------- Question 2 -----------\n\n");

            Car car1 = new Car(101, "TATA", "JAQUAR", 2022, 4000000);

            car1.DisplayCarDetails();

            #endregion

            #region Question 3

            /** 
             3. Write a program to verify password entered by user
                • The password must be at least 6 characters long.
                • It must contain at least one uppercase letter.
                • It must contain at least one digit.
                • Display respective messages to validate password 
             */

            Console.WriteLine("\n\n------------------- Question 3 -----------\n\n");

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            ValidatePassword(password);

            #endregion
        }

        static void ValidatePassword(string password) {

            if (password.Length < 6) {
                Console.WriteLine("Password must be at least 6 characters long.");
                return;
            }

            bool isPasswordContainsUppercase = false;
            bool isPasswordContainsDigit = false;

            for (int i = 0; i < password.Length; i++) {
                if (!isPasswordContainsUppercase) {
                    if (password[i] >= 'A' && password[i] <= 'Z') {
                        isPasswordContainsUppercase = true;
                    }
                }

                if (!isPasswordContainsDigit) {
                    if (Int32.TryParse(password[i].ToString(), out int num)) {
                        isPasswordContainsDigit = true;
                    }
                }

                if (isPasswordContainsUppercase && isPasswordContainsDigit) {
                    break;
                }
            }

            if (!isPasswordContainsUppercase) {
                Console.WriteLine("Password must contain at least one uppercase letter.");
                return;
            }

            if (!isPasswordContainsDigit) {
                Console.WriteLine("Password must contain at least one digit.");
                return;
            }

            Console.WriteLine("\nPassword is valid.");
        }
    }
}
