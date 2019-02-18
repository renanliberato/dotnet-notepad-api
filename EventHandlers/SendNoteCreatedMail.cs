using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Events;
using dotnet_notepad_api.Services;
using MediatR;

namespace dotnet_notepad_api.EventHandlers
{
    public class SendNoteCreatedMail : INotificationHandler<NoteCreated>
    {
        private readonly SmtpClientFactory _smtpFactory;

        public SendNoteCreatedMail(SmtpClientFactory smtpFactory)
        {
            _smtpFactory = smtpFactory;
        }

        public Task Handle(NoteCreated notification, CancellationToken cancellationToken)
        {
            SmtpClient client = _smtpFactory.build();
            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("whoever@me.com");
            mailMessage.To.Add("receiver@me.com");
            mailMessage.Body = $"A new note was created:\n Title: {notification.Title} \n Description: {notification.Description}";
            mailMessage.Subject = $"New note created: {notification.Title}";
            client.Send(mailMessage);
            
            return Task.FromResult(true);
        }
    }
}