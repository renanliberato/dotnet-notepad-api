using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;

namespace WebAPI.Tests.CommandHandlers
{
    public class CreateNoteHandlerTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectly()
        {
            var notesRepo = new Mock<DbSet<Note>>();
            notesRepo.Setup(obj => obj.Add(It.IsAny<Note>())).Verifiable();

            var context = new Mock<NotepadContext>(new DbContextOptions<NotepadContext>());
            context.SetupGet(obj => obj.Notes).Returns(notesRepo.Object).Verifiable();
            context.Setup<Task<int>>(obj => obj.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1).Verifiable();

            var handler = new CreateNoteHandler(context.Object);

            var command = new CreateNote("mytitle", "mydescription");

            var result = await handler.Handle(command, new CancellationToken());

            notesRepo.Verify();
            context.Verify();

            Assert.Same(command.Title, result.Title);
            Assert.Same(command.Description, result.Description);
        }
    }
}