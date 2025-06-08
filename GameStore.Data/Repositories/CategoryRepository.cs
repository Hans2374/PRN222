using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GameStoreDbContext _context;

        public CategoryRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize, string? sortOrder = null)
        {
            var query = _context.Categories.AsQueryable();

            // Apply sorting BEFORE pagination
            query = sortOrder switch
            {
                "name" => query.OrderBy(c => c.Name),
                "name_desc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Id) // Default sorting
            };

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCategoriesCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> CategoryHasGamesAsync(int categoryId)
        {
            return await _context.Games.AnyAsync(g => g.CategoryId == categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}