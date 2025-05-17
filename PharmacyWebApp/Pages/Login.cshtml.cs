using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace PharmacyWebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var account = await _accountService.AuthenticateAsync(Email, Password);

            // Check if authentication failed or role is not allowed
            if (account == null || (account.Role != 2 && account.Role != 3))
            {
                ErrorMessage = "You do not have permission to do this function!";
                return Page();
            }

            // Successful login
            HttpContext.Session.SetString("Role", account.Role.ToString());
            HttpContext.Session.SetString("Email", account.EmailAddress ?? string.Empty);

            return RedirectToPage("/Medicines/Index");
        }

    }
}