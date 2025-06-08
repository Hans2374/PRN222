using GameStore.Data.Models;

namespace GameStore.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize, string? sortOrder = null);
        Task<int> GetTotalCategoriesCountAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}