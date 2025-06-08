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

        public async Task<List<Game>> GetGamesAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetGamesAsync(pageNumber, pageSize);
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _repository.GetGameByIdAsync(id);
        }

        public async Task AddGameAsync(Game game)
        {
            await _repository.AddGameAsync(game);
        }

        public async Task UpdateGameAsync(Game game)
        {
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
