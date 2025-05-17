using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PharmacyWebApp.Data.Repositories
{
    public interface IStoreAccountRepository
    {
        Task<StoreAccount> GetByEmailAndPasswordAsync(string email, string password);
    }
}