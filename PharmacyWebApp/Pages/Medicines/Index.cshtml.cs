using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyWebApp.Pages.Medicines
{
    public class IndexModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public IndexModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public List<MedicineInformation> Medicines { get; set; } = new List<MedicineInformation>();
        public int UserRole { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3; // Display 3 medicines per page

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            // Ensure page number is at least 1
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            CurrentPage = pageNumber;

            // Get total count to calculate total pages
            int totalCount = await _medicineService.GetTotalMedicineCountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            // Get paginated medicines
            Medicines = await _medicineService.GetMedicinesAsync(CurrentPage, PageSize);

            // Read role from session
            var roleString = HttpContext.Session.GetString("Role");
            UserRole = int.TryParse(roleString, out var role) ? role : 0;

            return Page();
        }
    }
}