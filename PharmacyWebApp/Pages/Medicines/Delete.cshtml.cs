using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.Threading.Tasks;

namespace PharmacyWebApp.Pages.Medicines
{
    public class DeleteModel : PageModel
    {
        private readonly IMedicineService _medicineService;

        public DeleteModel(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [BindProperty]
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
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

            await _medicineService.DeleteMedicineAsync(id);

            return RedirectToPage("./Index");
        }
    }
}