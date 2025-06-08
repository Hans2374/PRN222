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

        public async Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize, string? sortOrder = null)
        {
            return await _repository.GetCategoriesAsync(pageNumber, pageSize, sortOrder);
        }

        public async Task<int> GetTotalCategoriesCountAsync()
        {
            return await _repository.GetTotalCategoriesCountAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
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
            // Business logic: Check if category has games before deleting
            var hasGames = await _repository.CategoryHasGamesAsync(id);
            if (hasGames)
            {
                throw new InvalidOperationException("Cannot delete category because it contains games. Please remove or reassign all games from this category before deleting.");
            }

            await _repository.DeleteCategoryAsync(id);
        }
    }
}