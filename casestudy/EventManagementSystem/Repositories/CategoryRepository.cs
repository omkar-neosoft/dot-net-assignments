﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Repositories {
    public class CategoryRepository : ICategoryRepository {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync() {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id) {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category) {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category) {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id) {
            var category = await _context.Categories.FindAsync(id);
            if (category != null) {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
