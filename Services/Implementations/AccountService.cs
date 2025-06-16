using Repositories.Models;
using Repositories.UnitOfWork;
using Services.Interfaces;

namespace Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LionAccount?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var account = await _unitOfWork.LionAccounts.GetAccountByEmailAsync(email);

            if (account != null && account.Password == password)
                return account;

            return null;
        }

        public async Task<LionAccount?> GetAccountByIdAsync(int id)
        {
            return await _unitOfWork.LionAccounts.GetByIdAsync(id);
        }

        public async Task<LionAccount?> GetAccountByEmailAsync(string email)
        {
            return await _unitOfWork.LionAccounts.GetAccountByEmailAsync(email);
        }

        public async Task<List<LionAccount>> GetAllAccountsAsync()
        {
            return await _unitOfWork.LionAccounts.GetAllAsync();
        }

        public async Task<bool> CreateAccountAsync(LionAccount account)
        {
            try
            {
                await _unitOfWork.LionAccounts.CreateAsync(account);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAccountAsync(LionAccount account)
        {
            try
            {
                await _unitOfWork.LionAccounts.UpdateAsync(account);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _unitOfWork.LionAccounts.DeleteAsync(id);
        }

        public bool IsAuthorized(int roleId, string function)
        {
            // Role: Administrator = 1; Manager = 2; Staff = 3; Member = 4
            return function.ToLower() switch
            {
                "create" or "update" or "delete" => roleId == 1 || roleId == 2,
                "view" or "search" or "detail" => roleId == 1 || roleId == 2 || roleId == 3,
                "login" => true,
                _ => false
            };
        }
    }
}