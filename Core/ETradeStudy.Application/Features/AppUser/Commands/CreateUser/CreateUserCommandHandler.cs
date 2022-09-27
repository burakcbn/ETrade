using AutoMapper;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Exceptions;
using MediatR;
using Createuser = ETradeStudy.Application.DTOs.User.CreateUser;

namespace ETradeStudy.Application.Features.AppUser.Commands.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            Createuser createUser = _mapper.Map<Createuser>(request);
            CreateUserResponse response= await _userService.CreateAsync(createUser);
            return _mapper.Map<CreateUserCommandResponse>(response);
            
        }
    }
}
