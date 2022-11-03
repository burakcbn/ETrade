using ETradeStudy.Application.DTOs.Role;
using ETradeStudy.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface IRoleService
    {
        (List<RoleDto>,int) GetAllRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string id, string name);
    }
}
