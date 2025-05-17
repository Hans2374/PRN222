using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PharmacyWebApp.Data.Repositories
{
    public class MedicineInformationRepository : IMedicineInformationRepository
    {
        private readonly Lab2PharmaceuticalDbContext _context;
        public MedicineInformationRepository(Lab2PharmaceuticalDbContext context)
        {
            _context = context;
        }
        public async Task<List<MedicineInformation>> GetAllWithManufacturerAsync(int pageNumber, int pageSize)
        {
            return await _context.MedicineInformations
            .Include(m => m.Manufacturer)
            .OrderBy(m => m.MedicineId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.MedicineInformations.CountAsync();
        }
    }
}