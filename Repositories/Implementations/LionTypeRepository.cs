using Repositories.Basic;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Implementations
{
    public class LionTypeRepository : GenericRepository<LionType>, ILionTypeRepository
    {
        public LionTypeRepository() : base() { }

        public LionTypeRepository(SU25LionDBContext context) : base(context) { }

        public async Task<bool> DeleteAsync(int id)
        {
            var lionType = await GetByIdAsync(id);
            if (lionType == null) return false;

            return await RemoveAsync(lionType);
        }
    }
}