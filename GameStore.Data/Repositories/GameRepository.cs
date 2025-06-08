using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameStoreDbContext _context;

        public GameRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetGamesAsync(int pageNumber, int pageSize, string? sortOrder = null)
        {
            var query = _context.Games.Include(g => g.Category).AsQueryable();

            // Apply sorting BEFORE pagination
            query = sortOrder switch
            {
                "title" => query.OrderBy(g => g.Title),
                "title_desc" => query.OrderByDescending(g => g.Title),
                "price" => query.OrderBy(g => g.Price),
                "price_desc" => query.OrderByDescending(g => g.Price),
                "date" => query.OrderBy(g => g.ReleaseDate),
                "date_desc" => query.OrderByDescending(g => g.ReleaseDate),
                "category" => query.OrderBy(g => g.Category != null ? g.Category.Name : ""),
                "category_desc" => query.OrderByDescending(g => g.Category != null ? g.Category.Name : ""),
                _ => query.OrderBy(g => g.Id) // Default sorting
            };

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Category)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalGamesCountAsync()
        {
            return await _context.Games.CountAsync();
        }
    }
}