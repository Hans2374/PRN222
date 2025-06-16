using Repositories.Models;
using Repositories.UnitOfWork;
using Services.Interfaces;

namespace Services.Implementations
{
    public class LionTypeService : ILionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LionType>> GetAllTypesAsync()
        {
            return await _unitOfWork.LionTypes.GetAllAsync();
        }

        public async Task<LionType?> GetTypeByIdAsync(int id)
        {
            return await _unitOfWork.LionTypes.GetByIdAsync(id);
        }

        public async Task<bool> CreateTypeAsync(LionType lionType)
        {
            try
            {
                await _unitOfWork.LionTypes.CreateAsync(lionType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTypeAsync(LionType lionType)
        {
            try
            {
                await _unitOfWork.LionTypes.UpdateAsync(lionType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            return await _unitOfWork.LionTypes.DeleteAsync(id);
        }
    }
}