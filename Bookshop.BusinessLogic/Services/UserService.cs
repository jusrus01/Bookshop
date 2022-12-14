using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace Bookshop.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IMailService mailService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new Exception("Could not confirm email");
            }
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (await IsUserAlreadyRegisteredAsync(registerDto.Email))
            {
                throw new Exception("User is already registered");
            }
           
            var newUser = await CreateNewUserAsync(registerDto);

            await _userManager.AddToRoleAsync(newUser, BookshopRoles.Client);

            await SendConfirmationEmailAsync(newUser);
        }

        private async Task<bool> IsUserAlreadyRegisteredAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private async Task<ApplicationUser> CreateNewUserAsync(RegisterDto registerDto)
        {
            var newIdentityUser = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                Created = DateTime.UtcNow
            };

            var identityResult = await _userManager.CreateAsync(newIdentityUser, registerDto.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Failed to create an account");
            }

            return newIdentityUser;
        }

        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var request = _httpContextAccessor.HttpContext.Request;
            var applicationPath = $"{request.Scheme}://{request.Host}";
            var confirmationLink = $"{applicationPath}/Client/ConfirmEmail?token={encodedToken}&email={user.Email}";

            _ = Task.Run(() => _mailService.SendEmailAsync(
                "Confirm your account",
                user.UserName,
                user.Email,
                confirmationLink));
        }

        public async Task SignInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            
            if (!result.Succeeded)
            {
                throw new Exception("Account not confirmed");
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}