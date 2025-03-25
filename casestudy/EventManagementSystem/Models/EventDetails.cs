using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Models {
    public class EventDetails {
        [Key, ForeignKey("Event")]
        public int Id {
            get; set;
        }

        [Required]
        public string Location {
            get; set;
        }

        [Required]
        public DateTime Date {
            get; set;
        }

        [Required]
        public int AvailableSeats {
            get; set;
        }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TicketPrice {
            get; set;
        }

        public Event Event {
            get; set;
        }
    }
}
