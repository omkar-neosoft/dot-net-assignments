using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsHackathonDay8.Models {
    public enum PolicyType {
        Life, Health, Vehicle, Property
    }

    public class Policy {
        public int PolicyID {
            get; set;
        }
        public string PolicyHolderName {
            get; set;
        }
        public PolicyType Type {
            get; set;
        }
        public DateTime StartDate {
            get; set;
        }
        public DateTime EndDate {
            get; set;
        }

        public Policy(int policyID, string policyHolderName, PolicyType type, DateTime startDate, DateTime endDate) {
            PolicyID = policyID;
            PolicyHolderName = policyHolderName;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Policy() {
        }

        public bool IsActive() {
            return DateTime.Now >= StartDate && DateTime.Now <= EndDate;
        }

        public override string ToString() {
            return $"| {PolicyID,-10} | {PolicyHolderName,-15} | {Type,-10} | {StartDate.ToString("yyyy-MM-dd"),-10} | {EndDate.ToString("yyyy-MM-dd"),-10} | {IsActive(),-10} |";
        }
    }
}
