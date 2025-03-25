﻿using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.ViewModels {
    public class RegisterViewModel {
        [Required]
        public string FirstName {
            get; set;
        }

        [Required]
        public string LastName {
            get; set;
        }

        [Required]
        [EmailAddress]
        public string Email {
            get; set;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password {
            get; set;
        }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword {
            get; set;
        }
    }
}
