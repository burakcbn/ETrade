using MediatR;

namespace ETradeStudy.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserCommandRequest:IRequest<GetRolesToUserCommandResponse>
    {
        public string UserId { get; set; }
    }
}