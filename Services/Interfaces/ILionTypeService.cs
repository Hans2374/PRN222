using Repositories.Models;

namespace Services.Interfaces
{
    public interface ILionTypeService
    {
        Task<List<LionType>> GetAllTypesAsync();
        Task<LionType?> GetTypeByIdAsync(int id);
        Task<bool> CreateTypeAsync(LionType lionType);
        Task<bool> UpdateTypeAsync(LionType lionType);
        Task<bool> DeleteTypeAsync(int id);
    }
}