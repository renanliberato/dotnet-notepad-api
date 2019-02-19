using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Events;
using WebAPI.Services;
using MediatR;
using WebAPI.Mails;
using WebAPI.Interfaces.Mails;

namespace WebAPI.EventHandlers
{
    public class SendNoteCreatedMail : INotificationHandler<NoteCreated>
    {
        private readonly IMailClient _mailClient;

        public SendNoteCreatedMail(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        public Task Handle(NoteCreated notification, CancellationToken cancellationToken)
        {
            _mailClient.Send(new NoteCreatedMail(
                notification.Title,
                notification.Description
            ));
            
            return Task.FromResult(true);
        }
    }
}