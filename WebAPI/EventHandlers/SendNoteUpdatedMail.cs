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
    public class SendNoteUpdatedMail : INotificationHandler<NoteUpdated>
    {
        private readonly IMailClient _mailClient;

        public SendNoteUpdatedMail(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        public Task Handle(NoteUpdated notification, CancellationToken cancellationToken)
        {
            _mailClient.Send(new NoteUpdatedMail(
                notification.Title,
                notification.Description
            ));
            
            return Task.FromResult(true);
        }
    }
}