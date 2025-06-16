using Repositories.Interfaces;

namespace Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ILionAccountRepository LionAccounts { get; }
        ILionProfileRepository LionProfiles { get; }
        ILionTypeRepository LionTypes { get; }

        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}