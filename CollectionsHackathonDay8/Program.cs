using System.Text;
using CollectionsHackathonDay8.Interface;
using CollectionsHackathonDay8.Models;
using CollectionsHackathonDay8.Repository;

namespace CollectionsHackathonDay8 {
    internal class Program {
        static void Main(string[] args) {
            /*
             * CREATE DATABASE InsuranceDB;
                GO
                USE InsuranceDB;
                GO
            * CREATE TABLE Policies (
                PolicyID INT IDENTITY(1,1) PRIMARY KEY,
                PolicyHolderName NVARCHAR(100) NOT NULL,
                PolicyType NVARCHAR(50) CHECK (PolicyType IN ('Life', 'Health', 'Vehicle', 'Property')) NOT NULL,
                StartDate DATE NOT NULL,
                EndDate DATE NOT NULL
              );


             */

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
                            //Console.Write("\nEnter Policy ID: ");
                            //int policyID = int.Parse(Console.ReadLine());
                            Console.Write("Enter Policy Holder Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Policy Type (Life, Health, Vehicle, Property): ");
                            string inputType = Console.ReadLine();
                            if (Enum.TryParse(typeof(PolicyType), inputType, true, out object result) && Enum.IsDefined(typeof(PolicyType), result)) {
                                PolicyType type = (PolicyType)result;
                            } else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[✖] Error: Invalid policy type entered. Please enter one of (Life, Health, Vehicle, Property).");
                                Console.ResetColor();
                                break;
                            }
                            //Console.Write("Enter Start Date (yyyy-mm-dd): ");
                            //DateTime startDate = DateTime.Parse(Console.ReadLine());
                            DateTime startDate = DateTime.Parse(DateTime.Now.ToString("MM-dd-yyyy"));
                            Console.Write("Enter End Date (yyyy-mm-dd): ");
                            string endDateString = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(endDateString) || !DateTime.TryParse(endDateString, out DateTime endDate)) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[✖] Error: Invalid date entered!");
                                Console.ResetColor();
                                break;

                            }
                            //DateTime endDate = DateTime.Parse(endDateString);
                            if (startDate >= endDate) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[✖] Error: Policy start date should be grater than end date!");
                                Console.ResetColor();
                                break;
                            }

                            policyRepo.AddPolicy(new Policy(name, type, startDate, endDate));
                            break;

                        case 2:
                            List<Policy> policies = policyRepo.ViewAllPolicies();
                            if (policies.Count == 0) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[✖] Error:No policies found.");
                                Console.ResetColor();
                            } else {
                                Console.WriteLine($"| {"ID",-10} | {"Name",-15} | {"Type",-10} | {"Start",-10} | {"End",-10} | {"Active",-10} |");
                                policies.ForEach(p => {
                                    Console.WriteLine(p);
                                });
                            }
                            break;

                        case 3:
                            Console.Write("\nEnter Policy ID: ");
                            int searchID = int.Parse(Console.ReadLine());
                            //var policy = policyRepo.SearchPolicyById(searchID);
                            Policy policy = policyRepo.FindPolicyById(searchID);
                            if (policy.PolicyID == 0) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[✖] Error: Policy not found!");
                                Console.ResetColor();
                                break;
                            }
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
                            List<Policy> activePolicies = policyRepo.ViewActivePolicies();
                            if (activePolicies.Count == 0) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[✖] Error:No policies found.");
                                Console.ResetColor();
                            } else {
                                Console.WriteLine($"| {"ID",-10} | {"Name",-15} | {"Type",-10} | {"Start",-10} | {"End",-10} | {"Active",-10} |");
                                activePolicies.ForEach(p => {
                                    Console.WriteLine(p);
                                });
                            }
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
