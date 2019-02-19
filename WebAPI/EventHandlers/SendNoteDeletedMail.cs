using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Events;
using dotnet_notepad_api.Services;
using MediatR;

namespace dotnet_notepad_api.EventHandlers
{
    public class SendNoteDeletedMail : INotificationHandler<NoteDeleted>
    {
        private readonly SmtpClientFactory _smtpFactory;

        public SendNoteDeletedMail(SmtpClientFactory smtpFactory)
        {
            _smtpFactory = smtpFactory;
        }

        public Task Handle(NoteDeleted notification, CancellationToken cancellationToken)
        {
            SmtpClient client = _smtpFactory.build();
            
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