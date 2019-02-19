using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Tests.CommandHandlers
{
    public class DeleteNoteCommandHandlerTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectlyWhenExists()
        {
            var noteMock = new Mock<Note>();
            noteMock.Setup(obj => obj.IsOwnedBy(It.IsAny<string>())).Returns(true);

            var notesRepo = new Mock<DbSet<Note>>();
            notesRepo.Setup(obj => obj.Find(It.IsAny<int>())).Returns(noteMock.Object);
            notesRepo.Setup(obj => obj.Remove(It.IsAny<Note>())).Verifiable();

            var context = new Mock<NotepadContext>(new DbContextOptions<NotepadContext>());
            context.SetupGet(obj => obj.Notes).Returns(notesRepo.Object);
            context.Setup(obj => obj.Remove(It.IsAny<Note>())).Verifiable();
            context.Setup<Task<int>>(obj => obj.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var handler = new DeleteNoteHandler(context.Object);

            var command = new DeleteNote(1);
            command.UserId = "myuserid";

            var result = await handler.Handle(command, new CancellationToken());

            noteMock.Verify(obj => obj.IsOwnedBy(command.UserId), Times.Once());

            notesRepo.Verify(obj => obj.Find(1), Times.Once());

            notesRepo.Verify(obj => obj.Remove(noteMock.Object), Times.Once());
            context.Verify(obj => obj.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

            Assert.True(result);
        }
    }
}