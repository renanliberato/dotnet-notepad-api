using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Commands;
using WebAPI.Services;

namespace WebAPI.CommandHandlers
{
    public class LoginHandler : IRequestHandler<Login, JwtSecurityToken>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<JwtSecurityToken> Handle(Login request, CancellationToken cancellationToken)
        {
            return await _authService.Login(request.Email, request.Password);
        }
    }
}