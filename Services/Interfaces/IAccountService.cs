using Repositories.Models;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<LionAccount?> LoginAsync(string email, string password);
        Task<LionAccount?> GetAccountByIdAsync(int id);
        Task<LionAccount?> GetAccountByEmailAsync(string email);
        Task<List<LionAccount>> GetAllAccountsAsync();
        Task<bool> CreateAccountAsync(LionAccount account);
        Task<bool> UpdateAccountAsync(LionAccount account);
        Task<bool> DeleteAccountAsync(int id);
        bool IsAuthorized(int roleId, string function);
    }
}