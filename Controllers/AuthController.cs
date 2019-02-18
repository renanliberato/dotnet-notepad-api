using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Events;
using dotnet_notepad_api.Helpers;
using dotnet_notepad_api.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_notepad_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        private readonly AppSettings _appSettings;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login command) 
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
                
                if (!result.Succeeded)
                {
                    return BadRequest("Could not create token");
                }
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_TOKEN));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_appSettings.JWT_ISSUER,
                _appSettings.JWT_ISSUER,
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest("User not found");
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Register command) 
        {
            var user = new User {
                UserName = command.Email,
                Email = command.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            
            if (result.Succeeded)
            {
                return Ok(new { Message = "OK"});
            } else {
                System.Console.WriteLine(result.Errors.ToString());
            }

            return BadRequest("Error");
        }
    }
}