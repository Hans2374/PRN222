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
            return await _repository.GetAllWithManufacturerAsync(pageNumber, pageSize);
        }

        public async Task<int> GetTotalMedicineCountAsync()
        {
            return await _repository.GetTotalCountAsync();
        }

        public async Task<MedicineInformation> GetMedicineByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            return await _repository.GetAllManufacturersAsync();
        }

        public async Task CreateMedicineAsync(MedicineInformation medicine)
        {
            await _repository.CreateAsync(medicine);
        }

        public async Task UpdateMedicineAsync(MedicineInformation medicine)
        {
            await _repository.UpdateAsync(medicine);
        }

        public async Task DeleteMedicineAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}