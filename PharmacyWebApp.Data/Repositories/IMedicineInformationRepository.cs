using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyWebApp.Data.Repositories
{
    public interface IMedicineInformationRepository
    {
        Task<List<MedicineInformation>> GetAllWithManufacturerAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<MedicineInformation> GetByIdAsync(string id);
        Task<List<Manufacturer>> GetAllManufacturersAsync();
        Task CreateAsync(MedicineInformation medicine);
        Task UpdateAsync(MedicineInformation medicine);
        Task DeleteAsync(string id);
    }
}