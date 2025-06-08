using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        // Redirect authenticated users to Games page
        return RedirectToPage("/Games/Index");
    }
}