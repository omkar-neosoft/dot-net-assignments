using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsHackathonDay8.Exceptions;
using CollectionsHackathonDay8.Interface;
using CollectionsHackathonDay8.Models;
using CollectionsHackathonDay8.Utility;
using static System.Reflection.Metadata.BlobBuilder;

namespace CollectionsHackathonDay8.Repository {
    public class PolicyRepository : IPolicyRepository {
        private static List<Policy> policies = new List<Policy>();
        SqlCommand cmd = null;
        string connstring;

        public PolicyRepository() {
            //policies.Add(new Policy(1, "Ram", PolicyType.Life, DateTime.Parse("2023-01-01"), DateTime.Parse("2028-01-01")));
            //policies.Add(new Policy(2, "Sham", PolicyType.Health, DateTime.Parse("2022-06-15"), DateTime.Parse("2027-06-15")));
            //policies.Add(new Policy(3, "Raj", PolicyType.Vehicle, DateTime.Parse("2024-03-10"), DateTime.Parse("2026-03-10")));
            //policies.Add(new Policy(4, "Taj", PolicyType.Property, DateTime.Parse("2023-09-20"), DateTime.Parse("2029-09-20")));
            //cmd = new SqlCommand();
            connstring = DbConnUtil.GetConnectionString();
        }

        public void AddPolicy(Policy policy) {
            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                cmd.CommandText = "insert into Policies(PolicyHolderName, PolicyType, StartDate, EndDate) values(@PolicyHolderName, @PolicyType, @StartDate, @EndDate)";
                cmd.Parameters.AddWithValue("@PolicyHolderName", policy.PolicyHolderName);
                cmd.Parameters.AddWithValue("@PolicyType", policy.Type.ToString());
                cmd.Parameters.AddWithValue("@StartDate", policy.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy added successfully.");
            Console.ResetColor();
        }

        public List<Policy> ViewAllPolicies() {
            List<Policy> policies = new List<Policy>();
            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Policies";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Policy Policy = new Policy();
                    Policy.PolicyID = reader.GetInt32("PolicyID");
                    Policy.PolicyHolderName = reader.GetString("PolicyHolderName");
                    Policy.Type = (PolicyType)Enum.Parse(typeof(PolicyType), reader.GetString("PolicyType"), true);

                    Policy.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    Policy.EndDate = Convert.ToDateTime(reader["EndDate"]);

                    policies.Add(Policy);
                }

                return policies;
            }
        }

        public Policy FindPolicyById(int policyID) {
            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                cmd.CommandText = "select * from Policies where PolicyID = @PolicyID";
                cmd.Parameters.AddWithValue("@PolicyID", policyID);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows) {
                    throw new PolicyNotFoundException($"Policy with ID {policyID} not found.");
                }
                Policy policy = new Policy();
                while (reader.Read()) {
                    policy.PolicyID = reader.GetInt32("PolicyID");
                    policy.PolicyHolderName = reader.GetString("PolicyHolderName");
                    policy.Type = (PolicyType)Enum.Parse(typeof(PolicyType), reader.GetString("PolicyType"), true);
                    policy.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    policy.EndDate = Convert.ToDateTime(reader["EndDate"]);
                }
                return policy;
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
            var policy = FindPolicyById(policyID);

            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                List<string> args = new List<string>();

                Console.Write("Enter New Policy Holder Name (Leave blank to keep unchanged): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName)) {
                    args.Add("PolicyHolderName = @PolicyHolderName");
                    cmd.Parameters.AddWithValue("@PolicyHolderName", newName);
                }

                Console.Write("Enter New Policy Type (Leave blank to keep unchanged): ");
                string newTypeInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTypeInput) && Enum.TryParse(newTypeInput, true, out PolicyType newType)) {

                    args.Add("PolicyType = @PolicyType");
                    cmd.Parameters.AddWithValue("@PolicyType", newType.ToString());
                }

                Console.Write("Enter New Start Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
                string newStartInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStartInput) && DateTime.TryParse(newStartInput, out DateTime newStart)) {
                    args.Add("StartDate = @StartDate");
                    cmd.Parameters.AddWithValue("@StartDate", newStart);
                }

                Console.Write("Enter New End Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
                string newEndInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEndInput) && DateTime.TryParse(newEndInput, out DateTime newEnd)) {
                    args.Add("EndDate = @EndDate");
                    cmd.Parameters.AddWithValue("@EndDate", policy.EndDate);
                }

                if (args.Count == 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[✖] Error: No fields to update!");
                    Console.ResetColor();
                    return;
                }

                cmd.CommandText = $"update Policies set {string.Join(", ", args)} where PolicyID = @PolicyID";
                cmd.Parameters.AddWithValue("@PolicyID", policyID);

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy updated successfully.");
            Console.ResetColor();
        }

        public void DeletePolicy(int policyID) {
            FindPolicyById(policyID);
            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Policies WHERE PolicyID = @PolicyID";
                cmd.Parameters.AddWithValue("@PolicyID", policyID);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Success: Policy deleted successfully.");
            Console.ResetColor();
        }

        public List<Policy> ViewActivePolicies() {
            List<Policy> policies = new List<Policy>();
            using (SqlConnection sqlConnection = new SqlConnection(connstring)) {
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Policies WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()";
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Policy Policy = new Policy();
                    Policy.PolicyID = reader.GetInt32("PolicyID");
                    Policy.PolicyHolderName = reader.GetString("PolicyHolderName");
                    Policy.Type = (PolicyType)Enum.Parse(typeof(PolicyType), reader.GetString("PolicyType"), true);

                    Policy.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    Policy.EndDate = Convert.ToDateTime(reader["EndDate"]);

                    policies.Add(Policy);
                }

                return policies;
            }
        }


        //public void AddPolicy(Policy policy) {
        //    if (policies.Any(p => p.PolicyID == policy.PolicyID)) {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\n[✖] Error: Policy ID already exists!");
        //        Console.ResetColor();
        //        return;
        //    }
        //    if (policy.StartDate >= policy.EndDate) {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\n[✖] Error: Policy start date should be grater than end date!");
        //        Console.ResetColor();
        //        return;
        //    }
        //    policies.Add(policy);
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("\n[✔] Success: Policy added successfully.");
        //    Console.ResetColor();
        //}


        //public void UpdatePolicy(int policyID) {
        //    var policy = SearchPolicyById(policyID);
        //    Console.Write("Enter New Policy Holder Name (Leave blank to keep unchanged): ");
        //    string newName = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(newName))
        //        policy.PolicyHolderName = newName;

        //    Console.Write("Enter New Policy Type (Leave blank to keep unchanged): ");
        //    string newTypeInput = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(newTypeInput) && Enum.TryParse(newTypeInput, true, out PolicyType newType))
        //        policy.Type = newType;

        //    Console.Write("Enter New Start Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
        //    string newStartInput = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(newStartInput) && DateTime.TryParse(newStartInput, out DateTime newStart))
        //        policy.StartDate = newStart;

        //    Console.Write("Enter New End Date (yyyy-mm-dd) (Leave blank to keep unchanged): ");
        //    string newEndInput = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(newEndInput) && DateTime.TryParse(newEndInput, out DateTime newEnd)) {
        //        if (policy.StartDate >= newEnd) {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("\n[✖] Error: Policy start date should be earlier than end date!");
        //            Console.ResetColor();
        //            return;
        //        }
        //        policy.EndDate = newEnd;
        //    }

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("\n[✔] Success: Policy updated successfully.");
        //    Console.ResetColor();
        //}


        //public void DeletePolicy(int policyID) {
        //    var policy = SearchPolicyById(policyID);
        //    policies.Remove(policy);
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("\n[✔] Success: Policy deleted successfully.");
        //    Console.ResetColor();
        //}

        // Method 1
        //public List<Policy> ViewActivePolicies() {
        //    List<Policy> policies = ViewAllPolicies();
        //    List<Policy> activePolicies = policies.Where(p => p.IsActive()).ToList();
        //    if (activePolicies.Count == 0) {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\n[✖] Error: No active policies found.");
        //        Console.ResetColor();
        //    } else
        //        activePolicies.ForEach(p => Console.WriteLine(p));
        //    return activePolicies;
        //}

        // Method 2

        //public void ViewActivePolicies() {
        //    var activePolicies = policies.Where(p => p.IsActive()).ToList();
        //    if (activePolicies.Count == 0) {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\n[✖] Error: No active policies found.");
        //        Console.ResetColor();
        //    } else
        //        activePolicies.ForEach(p => Console.WriteLine(p));
        //}
    }
}
