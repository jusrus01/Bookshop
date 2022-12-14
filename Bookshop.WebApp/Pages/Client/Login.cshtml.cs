using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BookshopPageModel
    {
        [BindProperty]
        public LoginViewModel LoginInput { get; set; } = new();

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public LoginModel(IUserService userService, IMapper mapper, INotyfService notyfService)
            :
            base(notyfService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _userService.SignInAsync(_mapper.Map<LoginDto>(LoginInput));
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("..//Index");
        }
    }
}
