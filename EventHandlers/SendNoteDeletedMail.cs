using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Events;
using MediatR;

namespace dotnet_notepad_api.EventHandlers
{
    public class SendNoteDeletedMail : INotificationHandler<NoteDeleted>
    {
        public Task Handle(NoteDeleted notification, CancellationToken cancellationToken)
        {
            SmtpClient client = new SmtpClient(
                System.Environment.GetEnvironmentVariable("SMTP_HOST")
            );
            
            client.Port = 2525;

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
                System.Environment.GetEnvironmentVariable("SMTP_USER"),
                System.Environment.GetEnvironmentVariable("SMTP_PASSWORD")
            );
            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("whoever@me.com");
            mailMessage.To.Add("receiver@me.com");
            mailMessage.Body = $"A note was deleted:\n Id: {notification.Id}";
            mailMessage.Subject = $"Note deleted: {notification.Id}";
            client.Send(mailMessage);
            
            return Task.FromResult(true);
        }
    }
}