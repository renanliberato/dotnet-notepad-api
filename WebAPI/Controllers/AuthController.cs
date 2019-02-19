using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Commands;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login command) 
        {
            var token = await _mediator.Send(command);

            if (token != null)
            {
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest("User not found");
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Register command) 
        {

            var succeeded = await _mediator.Send(command);

            if (succeeded)
            {
                return Ok(new { Message = "OK"});
            }

            return BadRequest("Error");
        }
    }
}