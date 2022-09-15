using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Services;
using Microsoft.AspNetCore.Identity;

namespace Bookshop.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            if (await IsUserAlreadyRegisteredAsync(registerDto.Email))
            {
                return false;
            }

            var newIdentityUser = new IdentityUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var identityResult = await _userManager.CreateAsync(newIdentityUser, registerDto.Password);

            if (!identityResult.Succeeded)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(newIdentityUser, BookshopRoles.Client);

            return true;
        }

        public async Task<bool> SignInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return false;
            }

            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

            return true;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<bool> IsUserAlreadyRegisteredAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}