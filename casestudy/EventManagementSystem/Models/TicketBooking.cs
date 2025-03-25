using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models {
    public class TicketBooking {
        public int Id {
            get; set;
        }

        [Required]
        public string UserId {
            get; set;
        }
        public ApplicationUser User {
            get; set;
        }

        [Required]
        public int EventId {
            get; set;
        }
        public Event Event {
            get; set;
        }

        [Required]
        [Range(1, 10)]
        public int Quantity {
            get; set;
        }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status {
            get; set;
        } // "Confirmed", "Canceled"

        public bool IsDeleted { get; set; } = false;

        public Payment Payment {
            get; set;
        }
    }
}
