using PharmacyWebApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyWebApp.Services.Services
{
    public interface IMedicineService
    {
        Task<List<MedicineInformation>> GetMedicinesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalMedicineCountAsync();
    }
}