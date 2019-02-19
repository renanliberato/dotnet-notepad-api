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
    public class SendNoteCreatedMailTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectly()
        {
            var mailClient = new Mock<IMailClient>();
            mailClient.Setup(obj => obj.Send(It.IsAny<NoteCreatedMail>())).Verifiable();

            var handler = new SendNoteCreatedMail(mailClient.Object);

            var noteEvent = new NoteCreated(1, "mytitle", "mydescription");

            await handler.Handle(noteEvent, new CancellationToken());

            mailClient.Verify(
                obj => obj.Send(It.IsAny<NoteCreatedMail>()),
                Times.Once()
            );
        }
    }
}