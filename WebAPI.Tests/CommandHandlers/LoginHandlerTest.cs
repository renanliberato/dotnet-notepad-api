using System.Threading;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;
using WebAPI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Tests.CommandHandlers
{
    public class LoginHandlerTest
    {
        [Fact]
        public async void handlerShouldExecuteCorrectly()
        {
            var token = new JwtSecurityToken();

            var service = new Mock<IAuthService>();
            service.Setup(obj => obj.Login(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(token).Verifiable();

            var handler = new LoginHandler(service.Object);

            var command = new Login("myemail", "mypassword");

            var result = await handler.Handle(command, new CancellationToken());

            Assert.Equal(result, token);
            
            service.Verify();
        }
    }
}