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
        private readonly IMailService _mailService;

        public UserService(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (await IsUserAlreadyRegisteredAsync(registerDto.Email))
            {
                throw new Exception("User is already registered");
            }

            var newIdentityUser = new IdentityUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var identityResult = await _userManager.CreateAsync(newIdentityUser, registerDto.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Failed to create an account");
            }

            await _userManager.AddToRoleAsync(newIdentityUser, BookshopRoles.Client);
        }

        public async Task SignInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
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