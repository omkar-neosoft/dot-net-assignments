using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models {
    public class Event {
        public int Id {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string Name {
            get; set;
        }

        // Optional Event Image
        public string? ImagePath {
            get; set;
        } // Nullable

        // One-to-One with EventDetails
        public EventDetails EventDetails {
            get; set;
        }

        // Many-to-One with Organizer
        [Required]
        public string OrganizerId {
            get; set;
        }
        public ApplicationUser Organizer {
            get; set;
        }

        // Many-to-One with Category
        [Required]
        public int CategoryId {
            get; set;
        }
        public Category Category {
            get; set;
        }
        public bool IsDeleted { get; set; } = false; // Soft delete flag

        // One-to-Many with TicketBooking
        public ICollection<TicketBooking> TicketBookings {
            get; set;
        }

    }
}
