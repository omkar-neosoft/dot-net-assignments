using Assignment_5.Repository;

namespace Assignment_5 {
    internal class Program {
        static void Main(string[] args) {
            #region Question 1
            /** 
             * 1. Implement exception handling in C# to manage cases where a
                    user enters an incorrect or non-existent beneficiary account
                    number?
             */
            Console.WriteLine("\n\n------------------- Question 1 -----------\n\n");

            AccountValidator.ValidateAccountWithExceptionHandling();

            #endregion

            #region Question 2
            /**
             * 2. Develop a c# program to define a common structure for
                different vehicle insurance policies (Two-Wheeler, FourWheeler, Commercial) while allowing specific policies to
                implement their own premium calculation?
             */

            Console.WriteLine("\n\n------------------- Question 2 -----------\n\n");

            // Covariance
            VehicleInsurance twoWheelerPolicy = new TwoWheelerInsurance("POLICY1", 150000);
            VehicleInsurance fourWheelerPolicy = new FourWheelerInsurance("POLICY2", 500000);
            VehicleInsurance commercialPolicy = new CommercialInsurance("POLICY3", 1000000);

            Console.WriteLine($"Premium for Two-Wheeler Policy (Policy Number: {twoWheelerPolicy.PolicyNumber}): {twoWheelerPolicy.CalculatePremium()}");
            Console.WriteLine($"Premium for Four-Wheeler Policy (Policy Number: {fourWheelerPolicy.PolicyNumber}): {fourWheelerPolicy.CalculatePremium()}");
            Console.WriteLine($"Premium for Commercial Policy (Policy Number: {commercialPolicy.PolicyNumber}): {commercialPolicy.CalculatePremium()}");

            #endregion
        }
    }
}
