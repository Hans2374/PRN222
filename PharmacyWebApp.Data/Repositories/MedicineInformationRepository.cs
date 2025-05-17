using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
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
            // Ensure page number is at least 1
            if (pageNumber < 1) pageNumber = 1;

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