using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;
using SU25Lion.Hubs;
using SU25Lion.Pages.Shared;

namespace SU25Lion.Pages.LionProfile
{
    public class DeleteModel : BasePageModel
    {
        private readonly ILionProfileService _lionProfileService;
        private readonly IHubContext<LionHub> _hubContext;

        public DeleteModel(IAccountService accountService, ILionProfileService lionProfileService, IHubContext<LionHub> hubContext)
            : base(accountService)
        {
            _lionProfileService = lionProfileService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Repositories.Models.LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanDelete)
                return RedirectToPage("/AccessDenied");

            if (id == null)
                return NotFound();

            var profile = await _lionProfileService.GetProfileByIdAsync(id.Value);
            if (profile == null)
                return NotFound();

            LionProfile = profile;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanDelete)
                return RedirectToPage("/AccessDenied");

            if (LionProfile.LionProfileId == 0)
                return NotFound();

            var result = await _lionProfileService.DeleteProfileAsync(LionProfile.LionProfileId);

            if (result)
            {
                // Notify all connected clients via SignalR
                await _hubContext.Clients.All.SendAsync("ProfileDeleted", LionProfile.LionProfileId);
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Error deleting lion profile");
            // Reload the profile data if delete failed
            LionProfile = await _lionProfileService.GetProfileByIdAsync(LionProfile.LionProfileId);
            return Page();
        }
    }
}