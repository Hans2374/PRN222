using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace PharmacyWebApp.Pages.Medicines
{
    public class IndexModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public IndexModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public IPagedList<MedicineInformation> MedicinesPaged { get; set; }
        public List<MedicineInformation> Medicines { get; set; }

        public async Task<IActionResult> OnGetAsync(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 3;

            // Đảm bảo rằng pageNumber luôn hợp lệ
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int totalCount = await _medicineService.GetTotalMedicineCountAsync();
            Medicines = await _medicineService.GetMedicinesAsync(pageNumber, pageSize);

            MedicinesPaged = new StaticPagedList<MedicineInformation>(
                Medicines,
                pageNumber,
                pageSize,
                totalCount
            );

            return Page();
        }
    }
}