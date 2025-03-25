using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventManagementSystem.Models;

namespace EventManagementSystem.ViewModels {
    public class TicketBookingViewModel {
        public int Id {
            get; set;
        }

        [Required]
        public int EventId {
            get; set;
        }

        // Event Foreign Key
        public EventViewModel Event {
            get; set;
        }

        [NotMapped]
        public string Name {
            get; set;
        }

        public string UserId {
            get; set;
        } // Auto-set from logged-in user

        // Event Foreign Key
        public ApplicationUser User {
            get; set;
        }

        public string UserName {
            get; set;
        }

        [Required]
        [Range(1, 10, ErrorMessage = "You can book between 1 to 10 tickets.")]
        public int Quantity {
            get; set;
        }
        //public int NumberOfTickets {
        //    get; set;
        //}

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public string EventName {
            get; set;
        }  // Added Event Name
        public string EventLocation {
            get; set;
        }  // Added Event Location
        public DateTime EventDate {
            get; set;
        }  // Added Event Date
        public string OrganizerName {
            get; set;
        }  // Added Organizer Name
    }

}
