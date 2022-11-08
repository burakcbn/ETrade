using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Abstractions.Services.Configurations;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Domain.Entities;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly IEndpointRead _endpointRead;
        private readonly IEndpointWrite _endpointWrite;
        private readonly IMenuWrite _menuWrite;
        private readonly IMenuRead _menuRead;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointRead endpointRead, IEndpointWrite endpointWrite, IMenuWrite menuWrite, IMenuRead menuRead, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointRead = endpointRead;
            _endpointWrite = endpointWrite;
            _menuWrite = menuWrite;
            _menuRead = menuRead;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuRead.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await _menuWrite.AddAsync(_menu);

                await _menuWrite.SaveAsync();
            }

            Endpoint? endpoint = await _endpointRead.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                        .FirstOrDefault(m => m.Name == menu)
                        ?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu
                };

                await _endpointWrite.AddAsync(endpoint);
                await _endpointWrite.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointWrite.SaveAsync();

        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointRead.Table
                  .Include(e => e.Roles)
                  .Include(e => e.Menu)
                  .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint != null)
                return endpoint.Roles.Select(r => r.Name).ToList();
            return null;
        }
    }
}
