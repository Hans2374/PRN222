using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using SU25Lion.Pages.Shared;

namespace SU25Lion.Pages.LionProfile
{
    public class IndexModel : BasePageModel
    {
        private readonly ILionProfileService _lionProfileService;

        public IndexModel(IAccountService accountService, ILionProfileService lionProfileService)
            : base(accountService)
        {
            _lionProfileService = lionProfileService;
        }

        public List<Repositories.Models.LionProfile> LionProfiles { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public bool IsSearching { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public double? SearchWeight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTypeName { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, string? action = null)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Login");

            if (!CanView)
                return RedirectToPage("/AccessDenied");

            if (action == "search" && (SearchWeight.HasValue || !string.IsNullOrWhiteSpace(SearchTypeName)))
            {
                IsSearching = true;
                LionProfiles = await _lionProfileService.SearchProfilesAsync(SearchWeight, SearchTypeName);
            }
            else
            {
                CurrentPage = pageNumber;
                var result = await _lionProfileService.GetPaginatedProfilesAsync(pageNumber, 3);
                LionProfiles = result.profiles;
                TotalPages = result.totalPages;
            }

            return Page();
        }
    }
}