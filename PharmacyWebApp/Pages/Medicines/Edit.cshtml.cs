using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyWebApp.Pages.Medicines
{
    public class EditModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public EditModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [BindProperty]
        public MedicineInformation Medicine { get; set; } = default!;

        public List<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            // Check if user is logged in
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetMedicineByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            Medicine = medicine;
            Manufacturers = await _medicineService.GetAllManufacturersAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if user is logged in
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                Manufacturers = await _medicineService.GetAllManufacturersAsync();
                return Page();
            }

            await _medicineService.UpdateMedicineAsync(Medicine);

            return RedirectToPage("./Index");
        }
    }
}