using Repositories.Models;

namespace Repositories.Interfaces
{
    public interface ILionTypeRepository
    {
        Task<List<LionType>> GetAllAsync();
        Task<LionType> GetByIdAsync(int id);
        Task<int> CreateAsync(LionType lionType);
        Task<int> UpdateAsync(LionType lionType);
        Task<bool> DeleteAsync(int id);
    }
}