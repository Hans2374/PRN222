using PharmacyWebApp.Data.Models;
using System.Threading.Tasks;
namespace PharmacyWebApp.Services.Services
{
    public interface IAccountService
    {
        Task<StoreAccount> AuthenticateAsync(string email, string password);
    }
}