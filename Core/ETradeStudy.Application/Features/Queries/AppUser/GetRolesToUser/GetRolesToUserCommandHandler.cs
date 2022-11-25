using ETradeStudy.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserCommandHandler : IRequestHandler<GetRolesToUserCommandRequest, GetRolesToUserCommandResponse>
    {
        private readonly IUserService _userService;

        public GetRolesToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetRolesToUserCommandResponse> Handle(GetRolesToUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetRolesToUserAsync(request.UserId);
            return new()
            {
                Roles = userRoles,
            };
        }
    }
}
