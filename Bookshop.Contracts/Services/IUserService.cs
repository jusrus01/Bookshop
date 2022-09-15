using Bookshop.Contracts.DataTransferObjects.Users;

namespace Bookshop.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);

        Task<bool> SignInAsync(LoginDto loginDto);

        Task SignOutAsync();
    }
}
