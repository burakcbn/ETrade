using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appuser = ETradeStudy.Domain.Entities.Identity.AppUser;
using ETradeStudy.Application.Exceptions;
using ETradeStudy.Application.Abstractions.Token;
using ETradeStudy.Application.DTOs;
using ETradeStudy.Application.Abstractions.Services.Authentication;

namespace ETradeStudy.Application.Features.AppUser.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IInternalAuthentication _ınternalAuthentication;

        public LoginUserCommandHandler( IInternalAuthentication ınternalAuthentication)
        {
            _ınternalAuthentication = ınternalAuthentication;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _ınternalAuthentication.LoginAsync(request.UserNameOrEmail, request.Password,900);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token,
            };
         
        }
    }
}
