using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.Repositories;
using EventManagementSystem.ViewModels;

namespace EventManagementSystem.Services {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync() {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name });
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id) {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return category == null ? null : new CategoryViewModel { Id = category.Id, Name = category.Name };
        }

        public async Task AddCategoryAsync(CategoryViewModel categoryViewModel) {
            var category = new Category { Name = categoryViewModel.Name };
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryViewModel categoryViewModel) {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryViewModel.Id);
            if (category != null) {
                category.Name = categoryViewModel.Name;
                await _categoryRepository.UpdateCategoryAsync(category);
            }
        }

        public async Task DeleteCategoryAsync(int id) {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
