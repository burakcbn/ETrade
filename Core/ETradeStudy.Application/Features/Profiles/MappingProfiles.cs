using AutoMapper;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Features.Commands.AppUser.CreateUser;
using ETradeStudy.Application.Features.Commands.Basket.AddItemToBasket;
using ETradeStudy.Application.ViewModel.Baskets;

namespace ETradeStudy.Application.Features.Profiles
{
    public class Mappingprofiles : Profile
    {
        public Mappingprofiles()
        {
            CreateMap<AddItemToBasketCommandRequest, VM_Create_BasketItem>().ReverseMap();
            CreateMap<CreateUserCommandRequest, CreateUser>().ReverseMap();
            CreateMap<CreateUserCommandResponse, CreateUserResponse>().ReverseMap();
        }
    }
}
