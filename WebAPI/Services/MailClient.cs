using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebAPI.Helpers;
using WebAPI.Interfaces.Mails;

namespace WebAPI.Services
{
    public class MailClient : IMailClient
    {
        private readonly SmtpClient _client;

        public MailClient(IOptions<AppSettings> settings)
        {
            var appSettings = settings.Value;
            
            _client = new SmtpClient(appSettings.SMTP_HOST);
            _client.Port = 2525;

            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(
                appSettings.SMTP_USER,
                appSettings.SMTP_PASSWORD
            );
        }

        public void Send(MailMessage mail)
        {
            _client.Send(mail);
        }
    }
}