using AutoMapper;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Features.Commands.AppUser.CreateUser;

namespace ETradeStudy.Application.Features.Profiles
{
    public class Mappingprofiles : Profile
    {
        public Mappingprofiles()
        {
            CreateMap<CreateUserCommandRequest, CreateUser>().ReverseMap();
            CreateMap<CreateUserCommandResponse, CreateUserResponse>().ReverseMap();
        }
    }
}
