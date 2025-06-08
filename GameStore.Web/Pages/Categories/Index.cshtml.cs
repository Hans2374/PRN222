using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Pages_Categories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private const int PageSize = 3;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Category { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        // Sort order property for view
        public string NameSortOrder => SortOrder == "name" ? "name_desc" : "name";

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/AccessDenied");
            }

            var totalItems = await _categoryService.GetTotalCategoriesCountAsync();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            var categories = await _categoryService.GetCategoriesAsync(CurrentPage, PageSize);

            // Apply sorting
            Category = SortOrder switch
            {
                "name" => categories.OrderBy(c => c.Name).ToList(),
                "name_desc" => categories.OrderByDescending(c => c.Name).ToList(),
                _ => categories
            };

            return Page();
        }
    }
}