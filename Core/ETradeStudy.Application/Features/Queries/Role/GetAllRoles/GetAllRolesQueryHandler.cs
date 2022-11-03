using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            (List<RoleDto> roles, int count) = _roleService.GetAllRoles(request.Page, request.Size);

            return new()
            {
                Roles = roles,
                Count = count
            };
        }
    }
}
