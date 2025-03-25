using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.ViewModels {
    public class EventViewModel {
        public int Id {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string Name {
            get; set;
        }

        [NotMapped]
        public IFormFile? ImageFile {
            get; set;
        } // For image upload
        public string? ImagePath {
            get; set;
        } // Stores the image path

        [Required]
        public string OrganizerId {
            get; set;
        }

        [Required]
        public int CategoryId {
            get; set;
        }

        // EventDetails Fields
        [Required]
        public string Location {
            get; set;
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date {
            get; set;
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Available seats must be greater than zero.")]
        public int AvailableSeats {
            get; set;
        }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1.00, double.MaxValue, ErrorMessage = "Ticket price must be greater than zero.")]
        public decimal TicketPrice {
            get; set;
        }

        public bool IsDeleted { get; set; } = false; // Soft delete flag
    }
}
