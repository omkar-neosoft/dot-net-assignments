using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsHackathonDay8.Models;

namespace CollectionsHackathonDay8.Interface {
    public interface IPolicyRepository {
        void AddPolicy(Policy policy);
        List<Policy> ViewAllPolicies();
        Policy SearchPolicyById(int policyID);
        void UpdatePolicy(int policyID);
        void DeletePolicy(int policyID);
        void ViewActivePolicies();
    }
}
