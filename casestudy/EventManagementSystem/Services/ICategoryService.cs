using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.ViewModels;

namespace EventManagementSystem.Services {
    public interface ICategoryService {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryViewModel categoryViewModel);
        Task UpdateCategoryAsync(CategoryViewModel categoryViewModel);
        Task DeleteCategoryAsync(int id);
    }
}
