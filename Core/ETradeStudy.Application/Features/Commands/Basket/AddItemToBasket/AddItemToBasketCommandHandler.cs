using AutoMapper;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.ViewModel.Baskets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public AddItemToBasketCommandHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            VM_Create_BasketItem item= _mapper.Map<VM_Create_BasketItem>(request);
            await _basketService.AddItemToBasketAsync(item);
            return new();
        }
    }
}
