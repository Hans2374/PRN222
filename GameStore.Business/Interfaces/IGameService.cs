using GameStore.Data.Models;

namespace GameStore.Business.Interfaces
{
    public interface IGameService
    {
        Task<List<Game>> GetGamesAsync(int pageNumber, int pageSize);
        Task<Game> GetGameByIdAsync(int id);
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
    }
}
