using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Web.Pages
{
    public class AccessDeniedModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccessDeniedModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (action == "cancel")
            {
                // Just redirect back to Games page
                return RedirectToPage("/Games/Index");
            }
            else
            {
                // Sign out the current user if any
                await _signInManager.SignOutAsync();

                // Redirect to login page
                return RedirectToPage("/Login");
            }
        }
    }
}