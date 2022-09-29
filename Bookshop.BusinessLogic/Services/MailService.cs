using Bookshop.Contracts.DataTransferObjects.Mails;
using Bookshop.Contracts.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Bookshop.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly MailConfiguration _settings;

        public MailService(IConfiguration configuration)
        {
            _settings = configuration.GetSection("MailConfiguration")
                .Get<MailConfiguration>();
        }

        public async Task SendEmailAsync(string subject, string name, string email, string message)
        {
            if (!_settings.UseSmtp4Dev)
            {
                throw new NotImplementedException(
                    "Other SMTP hosts are not supported yet. Install and open Smtp4Dev (https://github.com/rnwood/smtp4dev)" +
                    "or implement usage with another external host, such as Google.");
            }

            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(name, email));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.None);

            client.Capabilities &= ~SmtpCapabilities.Pipelining;

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
