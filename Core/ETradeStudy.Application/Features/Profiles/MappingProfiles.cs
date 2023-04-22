using AutoMapper;
using ETradeStudy.Application.DTOs.Product;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Features.Commands.AppUser.CreateUser;
using ETradeStudy.Application.Features.Commands.Basket.AddItemToBasket;
using ETradeStudy.Application.Features.Commands.Supplier.CreateSupplier;
using ETradeStudy.Application.Features.Queries.Supplier.GetAllSupplier;
using ETradeStudy.Application.ViewModel.Baskets;
using ETradeStudy.Domain.Entities;

namespace ETradeStudy.Application.Features.Profiles
{
    public class Mappingprofiles : Profile
    {
        public Mappingprofiles()
        {
            CreateMap<AddItemToBasketCommandRequest, VM_Create_BasketItem>().ReverseMap();
            CreateMap<CreateUserCommandRequest, CreateUser>().ReverseMap();
            CreateMap<CreateUserCommandResponse, CreateUserResponse>().ReverseMap();
            CreateMap<Supplier,GetAllSupplierQueryResponse>().ReverseMap();
            CreateMap<Supplier,CreateSupplierCommandRequest>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
