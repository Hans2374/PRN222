using Repositories.Models;

namespace Repositories.Interfaces
{
    public interface ILionAccountRepository
    {
        Task<LionAccount> GetAccountByEmailAsync(string email);
        Task<LionAccount> GetAccountByUsernameAsync(string username);
        Task<LionAccount> GetByIdAsync(int id);
        Task<List<LionAccount>> GetAllAsync();
        Task<int> CreateAsync(LionAccount account);
        Task<int> UpdateAsync(LionAccount account);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckLoginAsync(string email, string password);
    }
}