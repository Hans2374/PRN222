using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using SU25Lion.Pages.Shared;
using System.ComponentModel.DataAnnotations;

namespace SU25Lion.Pages.LionProfile
{
    public class CreateModel : BasePageModel
    {
        private readonly ILionProfileService _lionProfileService;
        private readonly ILionTypeService _lionTypeService;

        public CreateModel(IAccountService accountService, ILionProfileService lionProfileService, ILionTypeService lionTypeService)
            : base(accountService)
        {
            _lionProfileService = lionProfileService;
            _lionTypeService = lionTypeService;
        }

        [BindProperty]
        public CreateLionProfileViewModel LionProfile { get; set; } = new();

        public SelectList? LionTypeList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanCreate)
                return RedirectToPage("/AccessDenied");

            await LoadLionTypes();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanCreate)
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

            var profile = new Repositories.Models.LionProfile
            {
                LionName = LionProfile.LionName,
                LionTypeId = LionProfile.LionTypeId,
                Weight = LionProfile.Weight,
                Characteristics = LionProfile.Characteristics,
                Warning = LionProfile.Warning
            };

            var result = await _lionProfileService.CreateProfileAsync(profile);

            if (result)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Error creating lion profile");
            await LoadLionTypes();
            return Page();
        }

        private async Task LoadLionTypes()
        {
            var types = await _lionTypeService.GetAllTypesAsync();
            LionTypeList = new SelectList(types, "LionTypeId", "LionTypeName");
        }

        public class CreateLionProfileViewModel
        {
            [Required(ErrorMessage = "Lion name is required")]
            [Display(Name = "Lion Name")]
            public string LionName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Lion type is required")]
            [Display(Name = "Lion Type")]
            public int LionTypeId { get; set; }

            [Required(ErrorMessage = "Weight is required")]
            [Range(30.01, double.MaxValue, ErrorMessage = "Weight must be greater than 30")]
            public double Weight { get; set; }

            [Required(ErrorMessage = "Characteristics is required")]
            public string Characteristics { get; set; } = string.Empty;

            [Required(ErrorMessage = "Warning is required")]
            public string Warning { get; set; } = string.Empty;
        }
    }
}