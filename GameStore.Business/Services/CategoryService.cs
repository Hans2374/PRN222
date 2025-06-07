using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using GameStore.Data.Repositories;

namespace GameStore.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _repository.GetCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _repository.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            // Add business logic validation if needed
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category name cannot be empty");
            }

            await _repository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            // Add business logic validation if needed
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category name cannot be empty");
            }

            await _repository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            // Could add business logic to check if category has games before deleting
            await _repository.DeleteCategoryAsync(id);
        }
    }
}