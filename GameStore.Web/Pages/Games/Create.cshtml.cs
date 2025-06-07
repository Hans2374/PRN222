using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Pages_Games
{
    [Authorize(Roles = "Admin,Manager")]
    public class CreateModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IGameService gameService, ICategoryService categoryService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetCategoriesAsync();
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
                return Page();
            }

            await _gameService.AddGameAsync(Game);

            return RedirectToPage("./Index");
        }
    }
}