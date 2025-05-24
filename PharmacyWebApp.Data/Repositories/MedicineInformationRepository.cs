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
                .OrderByDescending(m => m.MedicineId) // Changed to OrderByDescending to show newest first
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.MedicineInformations.CountAsync();
        }

        public async Task<MedicineInformation> GetByIdAsync(string id)
        {
            return await _context.MedicineInformations
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _context.Manufacturers.OrderBy(m => m.ManufacturerName).ToListAsync();
        }

        public async Task CreateAsync(MedicineInformation medicine)
        {
            await _context.MedicineInformations.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicineInformation medicine)
        {
            _context.MedicineInformations.Update(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var medicine = await _context.MedicineInformations.FindAsync(id);
            if (medicine != null)
            {
                _context.MedicineInformations.Remove(medicine);
                await _context.SaveChangesAsync();
            }
        }
    }
}