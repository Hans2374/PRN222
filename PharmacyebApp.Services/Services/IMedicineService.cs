using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyWebApp.Services.Services
{
    public interface IMedicineService
    {
        Task<List<MedicineInformation>> GetMedicinesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalMedicineCountAsync();
        Task<MedicineInformation> GetMedicineByIdAsync(string id);
        Task<List<Manufacturer>> GetAllManufacturersAsync();
        Task CreateMedicineAsync(MedicineInformation medicine);
        Task UpdateMedicineAsync(MedicineInformation medicine);
        Task DeleteMedicineAsync(string id);
    }
}