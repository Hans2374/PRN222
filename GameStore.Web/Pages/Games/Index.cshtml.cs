using GameStore.Business.Interfaces;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Web.Pages_Games
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private const int PageSize = 3;

        public IndexModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IList<Game> Game { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/AccessDenied");
            }

            var totalItems = await _gameService.GetTotalGamesCountAsync();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            Game = await _gameService.GetGamesAsync(CurrentPage, PageSize);
            return Page();
        }
    }
}