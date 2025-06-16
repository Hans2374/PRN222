using Repositories.Models;

namespace Repositories.Interfaces
{
    public interface ILionProfileRepository
    {
        Task<List<LionProfile>> GetAllWithTypeAsync();
        Task<LionProfile> GetByIdWithTypeAsync(int id);
        Task<List<LionProfile>> SearchAsync(double? weight, string lionTypeName);
        Task<int> CreateAsync(LionProfile profile);
        Task<int> UpdateAsync(LionProfile profile);
        Task<bool> DeleteAsync(int id);
        Task<(List<LionProfile>, int)> GetPaginatedAsync(int pageNumber, int pageSize);
    }
}