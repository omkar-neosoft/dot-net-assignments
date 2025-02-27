using Assignment_5.Exceptions;

namespace Assignment_5.Repository {

    internal class AccountValidator {
        public static void ValidateAccount(string accountNumber) {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new FormatException("Account number cannot be empty.");

            if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
                throw new FormatException("Account number must be a 10-digit number.");
        }

        public static bool DoesAccountExist(string accountNumber) {
            List<string> existingAccounts1 = new List<string>
                {
                    "1234567890", "9876543210", "1122334455"
                };

            string? existingAccountNumber = existingAccounts1.Find(existingAccountNumber => existingAccountNumber == accountNumber);
            if (existingAccountNumber != null) {
                return true;
            }
            return false;
        }

        public static string GetAccountNumber() {
            Console.Write("Enter Beneficiary Account Number: ");
            return Console.ReadLine();
        }

        public static void ValidateAccountWithExceptionHandling() {
            try {
                string accountNumber = GetAccountNumber();

                ValidateAccount(accountNumber);

                if (!DoesAccountExist(accountNumber)) {
                    throw new InvalidAccountException("The account number does not exist in the system.");
                }

                Console.WriteLine("Beneficiary account number is valid and exists.");
            } catch (InvalidAccountException ex) {
                Console.WriteLine("Error: " + ex.Message);
            } catch (Exception ex) {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
