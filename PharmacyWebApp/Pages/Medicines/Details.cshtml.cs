using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Threading.Tasks;

namespace PharmacyWebApp.Pages.Medicines
{
    public class DetailsModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public DetailsModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public int UserRole { get; set; }

        public MedicineInformation Medicine { get; set; } = default!;

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

            // Read role from session
            var roleString = HttpContext.Session.GetString("Role");
            UserRole = int.TryParse(roleString, out var role1) ? role1 : 0;

            return Page();
        }
    }
}