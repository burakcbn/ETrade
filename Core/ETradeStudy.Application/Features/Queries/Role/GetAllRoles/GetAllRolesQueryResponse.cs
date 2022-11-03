using ETradeStudy.Application.DTOs.Role;

namespace ETradeStudy.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryResponse
    {
        public List<RoleDto> Roles { get; set; }
        public int Count { get; set; }
    }
}