using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Implementations
{
    public class LionAccountRepository : GenericRepository<LionAccount>, ILionAccountRepository
    {
        public LionAccountRepository() : base() { }

        public LionAccountRepository(SU25LionDBContext context) : base(context) { }

        public async Task<LionAccount> GetAccountByEmailAsync(string email)
        {
            return await _context.LionAccounts
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<LionAccount> GetAccountByUsernameAsync(string username)
        {
            return await _context.LionAccounts
                .FirstOrDefaultAsync(a => a.UserName == username);
        }

        public async Task<bool> CheckLoginAsync(string email, string password)
        {
            var account = await GetAccountByEmailAsync(email);
            return account != null && account.Password == password;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await GetByIdAsync(id);
            if (account == null) return false;

            return await RemoveAsync(account);
        }
    }
}