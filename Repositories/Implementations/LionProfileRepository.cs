using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Implementations
{
    public class LionProfileRepository : GenericRepository<LionProfile>, ILionProfileRepository
    {
        public LionProfileRepository() : base() { }

        public LionProfileRepository(SU25LionDBContext context) : base(context) { }

        public async Task<List<LionProfile>> GetAllWithTypeAsync()
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .OrderByDescending(p => p.LionProfileId)
                .ToListAsync();
        }

        public async Task<LionProfile> GetByIdWithTypeAsync(int id)
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .FirstOrDefaultAsync(p => p.LionProfileId == id);
        }

        public async Task<List<LionProfile>> SearchAsync(double? weight, string lionTypeName)
        {
            var query = _context.LionProfiles
                .Include(p => p.LionType)
                .AsQueryable();

            if (weight.HasValue)
            {
                query = query.Where(p => p.Weight == weight.Value);
            }

            if (!string.IsNullOrWhiteSpace(lionTypeName))
            {
                query = query.Where(p => p.LionType.LionTypeName.Contains(lionTypeName));
            }

            return await query.OrderByDescending(p => p.LionProfileId).ToListAsync();
        }

        public async Task<(List<LionProfile>, int)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.LionProfiles.CountAsync();

            var profiles = await _context.LionProfiles
                .Include(p => p.LionType)
                .OrderByDescending(p => p.LionProfileId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (profiles, totalCount);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await GetByIdAsync(id);
            if (profile == null) return false;

            return await RemoveAsync(profile);
        }
    }
}