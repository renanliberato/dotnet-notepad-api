using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Events;
using WebAPI.Services;
using MediatR;

namespace WebAPI.EventHandlers
{
    public class SendNoteUpdatedMail : INotificationHandler<NoteUpdated>
    {
        private readonly SmtpClientFactory _smtpFactory;

        public SendNoteUpdatedMail(SmtpClientFactory smtpFactory)
        {
            _smtpFactory = smtpFactory;
        }

        public Task Handle(NoteUpdated notification, CancellationToken cancellationToken)
        {
            SmtpClient client = _smtpFactory.build();
            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("whoever@me.com");
            mailMessage.To.Add("receiver@me.com");
            mailMessage.Body = $"A note was updated:\n Title: {notification.Title} \n Description: {notification.Description}";
            mailMessage.Subject = $"Note updated: {notification.Title}";
            client.Send(mailMessage);
            
            return Task.FromResult(true);
        }
    }
}