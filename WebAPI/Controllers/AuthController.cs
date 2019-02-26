using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Commands;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login command) 
        {
            var token = await _service.Login(command.Email, command.Password);

            if (token != null)
            {
                return Ok(new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token)));
            }

            return BadRequest("User not found");
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Register command) 
        {

            var succeeded = await _service.Register(command.Email, command.Password);

            if (succeeded)
            {
                return Ok(new { Message = "OK"});
            }

            return BadRequest("Error");
        }
    }

    public class LoginResponse
    {
        public string token {get; set;}

        public LoginResponse(string token)
        {
            this.token = token;
        }
    }
}