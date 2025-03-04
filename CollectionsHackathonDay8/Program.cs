using System.Text;
using CollectionsHackathonDay8.Interface;
using CollectionsHackathonDay8.Models;
using CollectionsHackathonDay8.Repository;

namespace CollectionsHackathonDay8 {
    internal class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8; // Enable Unicode Support
            IPolicyRepository policyRepo = new PolicyRepository();
            while (true) {
                Console.WriteLine("\nInsurance Policy Management System");
                Console.WriteLine("1. Add Policy");
                Console.WriteLine("2. View All Policies");
                Console.WriteLine("3. Search Policy by ID");
                Console.WriteLine("4. Update Policy");
                Console.WriteLine("5. Delete Policy");
                Console.WriteLine("6. View Active Policies");
                Console.WriteLine("7. Clear Console");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice)) {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                try {
                    switch (choice) {
                        case 1:
                            Console.Write("\nEnter Policy ID: ");
                            int policyID = int.Parse(Console.ReadLine());
                            Console.Write("Enter Policy Holder Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Policy Type (Life, Health, Vehicle, Property): ");
                            PolicyType type = (PolicyType)Enum.Parse(typeof(PolicyType), Console.ReadLine(), true);
                            Console.Write("Enter Start Date (yyyy-mm-dd): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter End Date (yyyy-mm-dd): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());
                            policyRepo.AddPolicy(new Policy(policyID, name, type, startDate, endDate));
                            break;

                        case 2:
                            policyRepo.ViewAllPolicies();
                            break;

                        case 3:
                            Console.Write("\nEnter Policy ID: ");
                            int searchID = int.Parse(Console.ReadLine());
                            var policy = policyRepo.SearchPolicyById(searchID);
                            Console.WriteLine($"| {"ID",-10} | {"Name",-15} | {"Type",-10} | {"Start",-10} | {"End",-10} | {"Active",-10} |");
                            Console.WriteLine(policy);
                            break;

                        case 4:
                            Console.Write("\nEnter Policy ID: ");
                            int updateID = int.Parse(Console.ReadLine());
                            policyRepo.UpdatePolicy(updateID);
                            break;

                        case 5:
                            Console.Write("\nEnter Policy ID: ");
                            int deleteID = int.Parse(Console.ReadLine());
                            policyRepo.DeletePolicy(deleteID);
                            break;

                        case 6:
                            policyRepo.ViewActivePolicies();
                            break;

                        case 7:
                            Console.Clear();
                            break;

                        case 8:
                            return;

                        default:
                            Console.WriteLine("Invalid choice! Try again.");
                            break;
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
