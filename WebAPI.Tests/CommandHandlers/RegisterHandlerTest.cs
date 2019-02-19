using System.Threading;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;
using WebAPI.Services;

namespace WebAPI.Tests.CommandHandlers
{
    public class RegisterHandlerTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectly()
        {
            var manager = new Mock<IAuthService>();
            manager.Setup(obj => obj.Register(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(true).Verifiable();

            var handler = new RegisterHandler(manager.Object);

            var command = new Register("myemail", "mypassword");

            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result);
            
            manager.VerifyAll();
        }
    }
}