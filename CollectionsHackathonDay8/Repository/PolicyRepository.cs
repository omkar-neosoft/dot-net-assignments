using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsHackathonDay8.Exceptions;
using CollectionsHackathonDay8.Interface;
using CollectionsHackathonDay8.Models;

namespace CollectionsHackathonDay8.Repository {
    public class PolicyRepository : IPolicyRepository {
        private static List<Policy> policies = new List<Policy>();

        public void AddPolicy(Policy policy) {
            if (policies.Any(p => p.PolicyID == policy.PolicyID)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[✖] Error: Policy ID already exists!");
                Console.ResetColor();
                return;
            }
            if (policy.StartDate >= policy.EndDate) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[✖] Error: Policy start date should be grater than end date!");
                Console.ResetColor();
                return;
            }
            policies.Add(policy);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy added successfully.");
            Console.ResetColor();
        }

        public void ViewAllPolicies() {
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
        }

        public Policy SearchPolicyById(int policyID) {
            var policy = policies.FirstOrDefault(p => p.PolicyID == policyID);
            if (policy == null) {
                throw new PolicyNotFoundException("\n[✖] Error: Policy not found!");
            }
            return policy;
        }

        public void UpdatePolicy(int policyID) {
            var policy = SearchPolicyById(policyID);
            Console.Write("Enter New Policy Holder Name (Leave blank to keep unchanged): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                policy.PolicyHolderName = newName;

            Console.Write("Enter New Policy Type (Leave blank to keep unchanged): ");
            string newTypeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTypeInput) && Enum.TryParse(newTypeInput, true, out PolicyType newType))
                policy.Type = newType;

            Console.Write("Enter New Start Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
            string newStartInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStartInput) && DateTime.TryParse(newStartInput, out DateTime newStart))
                policy.StartDate = newStart;

            Console.Write("Enter New End Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
            string newEndInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEndInput) && DateTime.TryParse(newEndInput, out DateTime newEnd)) {
                if (policy.StartDate >= newEnd) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[✖] Error: Policy start date should be earlier than end date!");
                    Console.ResetColor();
                    return;
                }
                policy.EndDate = newEnd;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy updated successfully.");
            Console.ResetColor();
        }

        public void DeletePolicy(int policyID) {
            var policy = SearchPolicyById(policyID);
            policies.Remove(policy);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy deleted successfully.");
            Console.ResetColor();
        }

        public void ViewActivePolicies() {
            var activePolicies = policies.Where(p => p.IsActive()).ToList();
            if (activePolicies.Count == 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[✖] Error: No active policies found.");
                Console.ResetColor();
            } else
                activePolicies.ForEach(p => Console.WriteLine(p));
        }
    }
}
