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
    public class SendNoteDeletedMail : INotificationHandler<NoteDeleted>
    {
        private readonly IMailClient _mailClient;

        public SendNoteDeletedMail(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        public Task Handle(NoteDeleted notification, CancellationToken cancellationToken)
        {
            _mailClient.Send(new NoteDeletedMail(
                notification.Id
            ));
            
            return Task.FromResult(true);
        }
    }
}