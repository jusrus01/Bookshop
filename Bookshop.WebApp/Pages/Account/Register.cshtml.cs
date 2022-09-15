using Bookshop.Contracts.Services;
using Bookshop.WebApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookshop.Contracts.DataTransferObjects.Users;
using AutoMapper;

namespace Bookshop.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public RegisterViewModel RegisterInput { get; set; } = new();
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegisterModel(IUserService userService, IMapper mapper)
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

            var registerDto = _mapper.Map<RegisterDto>(RegisterInput);
            var isUserCreated = await _userService.RegisterAsync(registerDto);

            if (!isUserCreated)
            {
                return Page();
            }

            return RedirectToPage("Login");
        }
    }
}
