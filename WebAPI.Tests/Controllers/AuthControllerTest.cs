using System.Threading;
using Moq;
using Xunit;
using WebAPI.Commands;
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
        public async void LoginShouldExecuteCorrectlyWhenUserExists()
        {
            var token = new JwtSecurityToken();

            var service = new Mock<IAuthService>();
            service.Setup(obj => obj.Login(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(token).Verifiable();

            var controller = new AuthController(service.Object);

            var command = new Login("myemail", "mypassword");

            var result = (OkObjectResult) await controller.login(command);
            var loginResponse = (LoginResponse) result.Value;

            var expectedResultToken = new JwtSecurityTokenHandler().WriteToken(token);

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResultToken, loginResponse.token);
            
            service.Verify();
        }

        [Fact]
        public async void LoginShouldExecuteCorrectlyWhenUserNotExists()
        {
            JwtSecurityToken token = null;
            
            var service = new Mock<IAuthService>();
            service.Setup(obj => obj.Login(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(token).Verifiable();

            var controller = new AuthController(service.Object);

            var command = new Login("myemail", "mypassword");

            var result = (BadRequestObjectResult) await controller.login(command);

            Assert.Equal(400, result.StatusCode);
            
            service.Verify();
        }

        [Fact]
        public async void RegisterShouldExecuteCorrectlyWhenServiceSucceed()
        {
            var service = new Mock<IAuthService>();
            service.Setup(obj => obj.Register(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(true).Verifiable();

            var controller = new AuthController(service.Object);

            var command = new Register("myemail", "mypassword");

            var result = (OkObjectResult) await controller.register(command);

            Assert.Equal(200, result.StatusCode);
            
            service.Verify();
        }

        [Fact]
        public async void RegisterShouldExecuteCorrectlyWhenServiceFails()
        {
            var mediator = new Mock<IAuthService>();
            mediator.Setup(obj => obj.Register(
                It.Is<string>(it => it == "myemail"),
                It.Is<string>(it => it == "mypassword")
            )).ReturnsAsync(false).Verifiable();

            var controller = new AuthController(mediator.Object);

            var command = new Register("myemail", "mypassword");

            var result = (BadRequestObjectResult) await controller.register(command);

            Assert.Equal(400, result.StatusCode);
            
            mediator.Verify();
        }
    }
}