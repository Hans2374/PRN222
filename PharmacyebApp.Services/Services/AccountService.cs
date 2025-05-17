using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Data.Repositories;
using System.Threading.Tasks;
namespace PharmacyWebApp.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IStoreAccountRepository _repository;
        public AccountService(IStoreAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<StoreAccount> AuthenticateAsync(string
        email, string password)
        {
            return await _repository.GetByEmailAndPasswordAsync(email, password);
        }
    }
}