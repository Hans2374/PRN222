using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using GameStore.Data.Repositories;

namespace GameStore.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Game>> GetGamesAsync(int pageNumber, int pageSize, string? sortOrder = null)
        {
            return await _repository.GetGamesAsync(pageNumber, pageSize, sortOrder);
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _repository.GetGameByIdAsync(id);
        }

        public async Task AddGameAsync(Game game)
        {
            // Add business logic validation if needed
            if (string.IsNullOrWhiteSpace(game.Title))
            {
                throw new ArgumentException("Game title cannot be empty");
            }

            if (game.Price < 0)
            {
                throw new ArgumentException("Game price cannot be negative");
            }

            await _repository.AddGameAsync(game);
        }

        public async Task UpdateGameAsync(Game game)
        {
            // Add business logic validation if needed
            if (string.IsNullOrWhiteSpace(game.Title))
            {
                throw new ArgumentException("Game title cannot be empty");
            }

            if (game.Price < 0)
            {
                throw new ArgumentException("Game price cannot be negative");
            }

            await _repository.UpdateGameAsync(game);
        }

        public async Task DeleteGameAsync(int id)
        {
            await _repository.DeleteGameAsync(id);
        }

        public async Task<int> GetTotalGamesCountAsync()
        {
            return await _repository.GetTotalGamesCountAsync();
        }
    }
}