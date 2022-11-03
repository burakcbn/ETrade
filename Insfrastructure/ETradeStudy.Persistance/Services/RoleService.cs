using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Role;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string id)
        {
            IdentityResult result = await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(id));
            return result.Succeeded;
        }

        public (List<RoleDto>, int) GetAllRoles(int page, int size)
        {
            return (_roleManager.Roles.Skip(page * size).Take(size).Select(x => new RoleDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList(), _roleManager.Roles.Count());
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async Task<bool> UpdateRole(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
