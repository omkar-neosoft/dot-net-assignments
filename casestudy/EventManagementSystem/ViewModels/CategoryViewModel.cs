using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.ViewModels {
    public class CategoryViewModel {
        public int Id {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string Name {
            get; set;
        }
    }
}
