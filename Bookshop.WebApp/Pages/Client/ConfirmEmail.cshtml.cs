using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Account
{
    public class ConfirmEmailModel : BookshopPageModel
    {
        private readonly IUserService _userService;

        public ConfirmEmailModel(IUserService userService) : base(null)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string token, string email)
        {
            try
            {
                await _userService.ConfirmEmailAsync(token, email);
                
                return RedirectToPage("Login");
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }
        }
    }
}
