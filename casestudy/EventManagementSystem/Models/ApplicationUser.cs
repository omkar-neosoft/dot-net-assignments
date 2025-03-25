using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventManagementSystem.Models {
    public class ApplicationUser : IdentityUser {
        [Required]
        public string FirstName {
            get; set;
        }

        [Required]
        public string LastName {
            get; set;
        }

        public bool IsDeleted { get; set; } = false;

        // One-to-Many relationship with TicketBooking
        public ICollection<TicketBooking> TicketBookings {
            get; set;
        }
    }
}
