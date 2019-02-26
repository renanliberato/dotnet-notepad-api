using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;
using WebAPI.Services;
using System.Net.Mail;
using WebAPI.EventHandlers;
using WebAPI.Events;
using WebAPI.Mails;
using WebAPI.Interfaces.Mails;

namespace WebAPI.Tests.CommandHandlers
{
    public class SendNoteDeletedMailTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectly()
        {
            var mailClient = new Mock<IMailClient>();
            mailClient.Setup(obj => obj.Send(It.IsAny<NoteDeletedMail>())).Verifiable();

            var handler = new SendNoteDeletedMail(mailClient.Object);

            var noteEvent = new NoteDeleted(1);

            await handler.Handle(noteEvent, new CancellationToken());

            mailClient.Verify();
        }
    }
}