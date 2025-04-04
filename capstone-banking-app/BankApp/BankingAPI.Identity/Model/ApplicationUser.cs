using Microsoft.AspNetCore.Identity;

namespace BankingAPI.Identity.Model {
    public class ApplicationUser : IdentityUser {
        public string FirstName {
            get; set;
        }
        public string LastName {
            get; set;
        }

    }
}
