using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PharmacyWebApp.Services.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineInformationRepository _repository;
        public MedicineService(IMedicineInformationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MedicineInformation>> GetMedicinesAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllWithManufacturerAsync(
             pageNumber, pageSize);
        }
        public async Task<int> GetTotalMedicineCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }
    }
}