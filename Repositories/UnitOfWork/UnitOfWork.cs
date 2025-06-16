using Repositories.Implementations;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SU25LionDBContext _context;
        private ILionAccountRepository _lionAccounts;
        private ILionProfileRepository _lionProfiles;
        private ILionTypeRepository _lionTypes;

        public UnitOfWork(SU25LionDBContext context)
        {
            _context = context;
        }

        public ILionAccountRepository LionAccounts
        {
            get
            {
                if (_lionAccounts == null)
                {
                    _lionAccounts = new LionAccountRepository(_context);
                }
                return _lionAccounts;
            }
        }

        public ILionProfileRepository LionProfiles
        {
            get
            {
                if (_lionProfiles == null)
                {
                    _lionProfiles = new LionProfileRepository(_context);
                }
                return _lionProfiles;
            }
        }

        public ILionTypeRepository LionTypes
        {
            get
            {
                if (_lionTypes == null)
                {
                    _lionTypes = new LionTypeRepository(_context);
                }
                return _lionTypes;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}