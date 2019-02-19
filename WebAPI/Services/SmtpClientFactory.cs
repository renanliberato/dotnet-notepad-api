using System.Net;
using System.Net.Mail;
using dotnet_notepad_api.Helpers;
using Microsoft.Extensions.Options;

namespace dotnet_notepad_api.Services
{
    public class SmtpClientFactory
    {
        private readonly AppSettings _appSettings;

        public SmtpClientFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public SmtpClient build()
        {
            SmtpClient client = new SmtpClient(
                _appSettings.SMTP_HOST
            );
            
            client.Port = 2525;

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
                _appSettings.SMTP_USER,
                _appSettings.SMTP_PASSWORD
            );

            return client;
        }
    }
}