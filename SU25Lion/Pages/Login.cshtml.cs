using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SU25Lion.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // Clear session on login page
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _accountService.LoginAsync(Email, Password);

            if (account == null)
            {
                ErrorMessage = "Invalid Email or Password!";
                return Page();
            }

            // Store user info in session
            HttpContext.Session.SetInt32("UserId", account.AccountId);
            HttpContext.Session.SetString("UserName", account.UserName);
            HttpContext.Session.SetString("FullName", account.FullName);
            HttpContext.Session.SetInt32("RoleId", account.RoleId);

            return RedirectToPage("/LionProfile/Index");
        }
    }
}