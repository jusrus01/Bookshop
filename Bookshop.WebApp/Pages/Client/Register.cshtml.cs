using Bookshop.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Bookshop.WebApp.PageModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.WebApp.ViewModels.Clients;
using Bookshop.Contracts.DataTransferObjects.Clients;

namespace Bookshop.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : BookshopPageModel
    {

        [BindProperty]
        public RegisterViewModel RegisterInput { get; set; } = new();
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegisterModel(IUserService userService, IMapper mapper, INotyfService notyfService)
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
                await _userService.RegisterAsync(_mapper.Map<RegisterDto>(RegisterInput));
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("Login");
        }
    }
}
