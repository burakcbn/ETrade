using AutoMapper;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Features.AppUser.Commands.CreateUser;
using Appuser = ETradeStudy.Domain.Entities.Identity.AppUser;

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
