using System.Net.Mail;

namespace WebAPI.Interfaces.Mails
{
    public interface IMailClient
    {
        void Send(MailMessage mail);
    }
}