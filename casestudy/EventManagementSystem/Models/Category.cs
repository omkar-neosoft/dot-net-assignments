using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models {
    public class Category {
        public int Id {
            get; set;
        }

        [Required]
        [StringLength(50)]
        public string Name {
            get; set;
        }
        public bool IsDeleted { get; set; } = false;
    }
}
