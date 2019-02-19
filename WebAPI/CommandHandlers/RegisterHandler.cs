using System.Threading;
using System.Threading.Tasks;
using WebAPI.Commands;
using WebAPI.Models;
using MediatR;
using WebAPI.Services;

namespace WebAPI.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<Register, bool>
    {
        private readonly IAuthService _authService;

        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(Register request, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(request.Email, request.Password);
            
            return result;
        }
    }
}