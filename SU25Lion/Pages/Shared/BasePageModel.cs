using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace SU25Lion.Pages.Shared
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly IAccountService _accountService;

        public BasePageModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        public string? CurrentUserName => HttpContext.Session.GetString("UserName");
        public string? CurrentFullName => HttpContext.Session.GetString("FullName");
        public int? CurrentRoleId => HttpContext.Session.GetInt32("RoleId");

        public bool IsAuthenticated() => CurrentUserId.HasValue;

        public bool CanCreate => CurrentRoleId.HasValue && _accountService.IsAuthorized(CurrentRoleId.Value, "create");
        public bool CanUpdate => CurrentRoleId.HasValue && _accountService.IsAuthorized(CurrentRoleId.Value, "update");
        public bool CanDelete => CurrentRoleId.HasValue && _accountService.IsAuthorized(CurrentRoleId.Value, "delete");
        public bool CanView => CurrentRoleId.HasValue && _accountService.IsAuthorized(CurrentRoleId.Value, "view");
        public bool CanSearch => CurrentRoleId.HasValue && _accountService.IsAuthorized(CurrentRoleId.Value, "search");
    }
}