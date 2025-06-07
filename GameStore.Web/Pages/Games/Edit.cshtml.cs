using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Pages_Games
{
    [Authorize(Roles = "Admin,Manager")]
    public class EditModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        public EditModel(IGameService gameService, ICategoryService categoryService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameService.GetGameByIdAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            Game = game;

            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetCategoriesAsync();
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
                return Page();
            }

            try
            {
                await _gameService.UpdateGameAsync(Game);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await GameExists(Game.Id);
                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> GameExists(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            return game != null;
        }
    }
}