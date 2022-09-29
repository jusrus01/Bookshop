namespace Bookshop.Contracts.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string subject, string name, string email, string message);
    }
}
