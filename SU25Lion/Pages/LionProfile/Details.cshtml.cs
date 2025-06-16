using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using SU25Lion.Pages.Shared;

namespace SU25Lion.Pages.LionProfile
{
    public class DetailModel : BasePageModel
    {
        private readonly ILionProfileService _lionProfileService;

        public DetailModel(IAccountService accountService, ILionProfileService lionProfileService)
            : base(accountService)
        {
            _lionProfileService = lionProfileService;
        }

        public Repositories.Models.LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanView)
                return RedirectToPage("/AccessDenied");

            if (id == null)
                return NotFound();

            var profile = await _lionProfileService.GetProfileByIdAsync(id.Value);
            if (profile == null)
                return NotFound();

            LionProfile = profile;
            return Page();
        }
    }
}