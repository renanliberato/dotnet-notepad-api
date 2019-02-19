using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class IdentityAuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        
        private readonly UserManager<User> _userManager;

        private readonly AppSettings _appSettings;

        public IdentityAuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<JwtSecurityToken> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            
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

        public async Task<bool> Register(string email, string password)
        {
            var user = new User {
                UserName = email,
                Email = password,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}