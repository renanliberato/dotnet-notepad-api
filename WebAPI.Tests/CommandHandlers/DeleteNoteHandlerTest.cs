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
    public class DeleteNoteHandlerTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectlyWhenExists()
        {
            var noteMock = new Mock<Note>();
            noteMock.Setup(obj => obj.IsOwnedBy(It.IsAny<string>())).Returns(true).Verifiable();

            var notesRepo = new Mock<DbSet<Note>>();
            notesRepo.Setup(obj => obj.Find(It.IsAny<int>())).Returns(noteMock.Object).Verifiable();
            notesRepo.Setup(obj => obj.Remove(It.IsAny<Note>())).Verifiable();

            var context = new Mock<NotepadContext>(new DbContextOptions<NotepadContext>());
            context.SetupGet(obj => obj.Notes).Returns(notesRepo.Object).Verifiable();
            context.Setup<Task<int>>(obj => obj.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1).Verifiable();

            var handler = new DeleteNoteHandler(context.Object);

            var command = new DeleteNote(1);
            command.UserId = "myuserid";

            var result = await handler.Handle(command, new CancellationToken());

            noteMock.Verify();
            notesRepo.Verify();
            context.Verify();

            Assert.True(result);
        }
        [Fact]
        public async void handlerShouldExecuteCorrectlyWhenNotExists()
        {
            var notesRepo = new Mock<DbSet<Note>>();
            notesRepo.Setup(obj => obj.Find(It.IsAny<int>()));

            var context = new Mock<NotepadContext>(new DbContextOptions<NotepadContext>());
            context.SetupGet(obj => obj.Notes).Returns(notesRepo.Object);

            var handler = new DeleteNoteHandler(context.Object);

            var command = new DeleteNote(1);
            command.UserId = "myuserid";

            var result = await handler.Handle(command, new CancellationToken());

            notesRepo.Verify(obj => obj.Find(1), Times.Once());

            Assert.False(result);
        }

        [Fact]
        public async void handlerShouldExecuteCorrectlyWhenNotAuthorized()
        {
            var noteMock = new Mock<Note>();
            noteMock.Setup(obj => obj.IsOwnedBy(It.IsAny<string>())).Returns(false);

            var notesRepo = new Mock<DbSet<Note>>();
            notesRepo.Setup(obj => obj.Find(It.IsAny<int>())).Returns(noteMock.Object);

            var context = new Mock<NotepadContext>(new DbContextOptions<NotepadContext>());
            context.SetupGet(obj => obj.Notes).Returns(notesRepo.Object);

            var handler = new DeleteNoteHandler(context.Object);

            var command = new DeleteNote(1);
            command.UserId = "myuserid";

            var result = await handler.Handle(command, new CancellationToken());

            noteMock.Verify(obj => obj.IsOwnedBy(command.UserId), Times.Once());

            notesRepo.Verify(obj => obj.Find(1), Times.Once());

            Assert.False(result);
        }
    }

}