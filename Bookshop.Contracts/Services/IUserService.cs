using Bookshop.Contracts.DataTransferObjects.Clients;

namespace Bookshop.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task SignInAsync(LoginDto loginDto);

        Task SignOutAsync();

        Task ConfirmEmailAsync(string token, string email);
    }
}
