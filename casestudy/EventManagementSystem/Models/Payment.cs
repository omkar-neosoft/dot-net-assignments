using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models {
    public class Payment {
        public int Id {
            get; set;
        }

        [Required]
        public int TicketBookingId {
            get; set;
        }
        public TicketBooking TicketBooking {
            get; set;
        }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount {
            get; set;
        }

        [Required]
        public string PaymentMethod {
            get; set;
        } // e.g., "Credit Card", "UPI"

        [Required]
        public string Status {
            get; set;
        } // "Paid", "Pending", "Failed"
    }
}
