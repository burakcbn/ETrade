using MediatR;

namespace ETradeStudy.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}