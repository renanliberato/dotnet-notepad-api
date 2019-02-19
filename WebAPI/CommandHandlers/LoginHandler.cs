using System.Threading;
using System.Threading.Tasks;
using WebAPI.Commands;
using WebAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Helpers;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.CommandHandlers
{
    public class LoginHandler : IRequestHandler<Login, JwtSecurityToken>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        private readonly AppSettings _appSettings;

        public LoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<JwtSecurityToken> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                
                if (!result.Succeeded)
                {
                    return null;
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

                return token;
            }

            return null;
        }
    }
}