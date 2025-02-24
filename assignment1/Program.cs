using Microsoft.VisualBasic;
using static System.Formats.Asn1.AsnWriter;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region question 1, 2:
            //You are developing a school management system. Each student has a name, age, and
            //percentage.Write a C# code to perform the following
            //     Choose the appropriate data types to store the information
            //     Ask the user to enter values for these fields.
            //     Display the information of student to console window
            //In the above scenario user might enter a non - numeric value for age, which halts the
            //execution
            // handle this situation and write a appropriate c# code for this.
            // Give proper message to the user if he enters non-numeric value

            Console.WriteLine("\n\n-------------------Question 1, 2 Input-----------\n\n");
            Console.WriteLine("Enter the name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the age: ");
            bool isAgeNumber = false;
            int age;
            isAgeNumber = int.TryParse(Console.ReadLine(),out age);
            

            Console.WriteLine("Enter the percentage: ");
            bool isPercentageNotInSting = false;
            double percentage;
            isPercentageNotInSting = double.TryParse(Console.ReadLine(), out percentage);

            Console.WriteLine("\n\n-------------------Question 1, 2 Output-----------\n\n");

            if (!isAgeNumber)
            {
                Console.WriteLine("Age should be in integer.");
            }

            if (!isPercentageNotInSting)
            {
                Console.WriteLine("Percentage should be in double.");
            }

            Console.WriteLine($"Student name: {name} \nStudent age: {age} \nStudent Percentage: {percentage}");

            #endregion

            #region Question 3

            Console.WriteLine("\n\n-------------------Question 3 Input-----------\n\n");

            Console.WriteLine("Enter the email: ");
            string email = default;
            email = Console.ReadLine();

            Console.WriteLine("\n\n-------------------Question 3 Output-----------\n\n");

            if (email == "")
            {
                Console.WriteLine("You should have to enter email");
            } else
            {
                Console.WriteLine($"Student email: {email}");
            }

            #endregion

            #region Question 4
            //You are building a hospital management system where a patient’s discharge date may not
            //always be available
            // The system should allow null values for the discharge date but still be able
            // to check if a patient has been discharged.
            // If the patient has the discharge date print it to the console, if discharge date is
            //null then print “Not Discharged” message to the console. 

            Console.WriteLine("\n\n-------------------Question 4 Output-----------\n\n");
            bool isPatientDischargeDate = false;
            //int? patientDischargeDate = Convert.ToInt32(Console.ReadLine());
            int? patient1DischargeDate = 20;
            int? patient2DischargeDate = null;
            if (patient1DischargeDate != null)
            {
                Console.WriteLine($"Patient1 discharge date is: {patient1DischargeDate}");
            } else
            {
                Console.WriteLine($"Patient1 Not Discharged");
            }

            if (patient2DischargeDate != null)
            {
                Console.WriteLine($"Patient2 discharge date is: {patient1DischargeDate}");
            }
            else
            {
                Console.WriteLine($"Patient2 Not Discharged");
            }
            #endregion
        }
    }
}
