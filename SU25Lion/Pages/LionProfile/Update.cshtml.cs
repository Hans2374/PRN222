using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using SU25Lion.Pages.Shared;

namespace SU25Lion.Pages.LionProfile
{
    public class UpdateModel : BasePageModel
    {
        private readonly ILionProfileService _lionProfileService;
        private readonly ILionTypeService _lionTypeService;

        public UpdateModel(IAccountService accountService, ILionProfileService lionProfileService, ILionTypeService lionTypeService)
            : base(accountService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        [BindProperty]
        public Repositories.Models.LionProfile LionProfile { get; set; } = default!;

        public SelectList? LionTypeList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanUpdate)
                return RedirectToPage("/AccessDenied");

            if (id == null)
                return NotFound();

            var profile = await _lionProfileService.GetProfileByIdAsync(id.Value);
            if (profile == null)
                return NotFound();

            LionProfile = profile;
            await LoadLionTypes();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanUpdate)
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
            {
                await LoadLionTypes();
                return Page();
            }

            // Additional validation
            var nameValidation = _lionProfileService.ValidateLionName(LionProfile.LionName);
            if (!string.IsNullOrEmpty(nameValidation))
            {
                ModelState.AddModelError("LionProfile.LionName", nameValidation);
                await LoadLionTypes();
                return Page();
            }

            var result = await _lionProfileService.UpdateProfileAsync(LionProfile);

            if (result)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Error updating lion profile");
            await LoadLionTypes();
            return Page();
        }

        private async Task LoadLionTypes()
        {
            var types = await _lionTypeService.GetAllTypesAsync();
            LionTypeList = new SelectList(types, "LionTypeId", "LionTypeName");
        }
    }
}