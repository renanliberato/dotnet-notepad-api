using System.Threading;
using Moq;
using WebAPI.Models;
using WebAPI.CommandHandlers;
using Xunit;
using WebAPI.Commands;
using System.Threading.Tasks;
using WebAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Tests.Controllers
{
    public class AuthControllerTest
    {
        [Fact]
        public async void loginShouldExecuteCorrectlyWhenUserExists()
        {
            var token = new JwtSecurityToken();

            var mediator = new Mock<IMediator>();
            mediator.Setup(obj => obj.Send(
                It.Is<Login>(it => it.Email == "myemail" && it.Password == "mypassword"),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(token).Verifiable();

            var controller = new AuthController(mediator.Object);

            var command = new Login("myemail", "mypassword");

            var result = (OkObjectResult) await controller.login(command);
            var loginResponse = (LoginResponse) result.Value;

            var expectedResultToken = new JwtSecurityTokenHandler().WriteToken(token);

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResultToken, loginResponse.token);
            
            mediator.Verify();
        }

        [Fact]
        public async void loginShouldExecuteCorrectlyWhenUserNotExists()
        {
            JwtSecurityToken token = null;
            
            var mediator = new Mock<IMediator>();
            mediator.Setup(obj => obj.Send(
                It.Is<Login>(it => it.Email == "myemail" && it.Password == "mypassword"),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(token).Verifiable();

            var controller = new AuthController(mediator.Object);

            var command = new Login("myemail", "mypassword");

            var result = (BadRequestObjectResult) await controller.login(command);

            Assert.Equal(400, result.StatusCode);
            
            mediator.Verify();
        }

        [Fact]
        public async void registerShouldExecuteCorrectlyWhenServiceSucceed()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(obj => obj.Send(
                It.Is<Register>(it => it.Email == "myemail" && it.Password == "mypassword"),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(true).Verifiable();

            var controller = new AuthController(mediator.Object);

            var command = new Register("myemail", "mypassword");

            var result = (OkObjectResult) await controller.register(command);

            Assert.Equal(200, result.StatusCode);
            
            mediator.Verify();
        }

        [Fact]
        public async void registerShouldExecuteCorrectlyWhenServiceFails()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(obj => obj.Send(
                It.Is<Register>(it => it.Email == "myemail" && it.Password == "mypassword"),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(false).Verifiable();

            var controller = new AuthController(mediator.Object);

            var command = new Register("myemail", "mypassword");

            var result = (BadRequestObjectResult) await controller.register(command);

            Assert.Equal(400, result.StatusCode);
            
            mediator.Verify();
        }
    }
}