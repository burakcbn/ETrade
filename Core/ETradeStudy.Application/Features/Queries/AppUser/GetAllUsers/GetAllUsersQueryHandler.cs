using ETradeStudy.Application.Abstractions.Services;
using MediatR;

namespace ETradeStudy.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var usersDto = _userService.GetAllUsers(request.Page, request.Size);
            return new GetAllUsersQueryResponse()
            {
                Count = usersDto.Count,
                Users = usersDto.Users,
            };
        }
    }
}
