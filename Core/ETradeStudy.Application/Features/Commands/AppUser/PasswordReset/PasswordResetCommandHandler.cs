using ETradeStudy.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.AppUser.PasswordReset
{
    internal class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        private readonly IAuthService _authService;

        public PasswordResetCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.PasswordResetAsync(request.Email);
            return new();
        }
    }
}
