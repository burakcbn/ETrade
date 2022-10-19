using ETradeStudy.Application.Abstractions.Hubs;
using ETradeStudy.Application.Abstractions.Services;
using MediatR;

namespace ETradeStudy.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {

            await _orderService.CreateOrderAsync(new()
            {
                BasketId = _basketService.GetUserActiveBasket?.Id.ToString(),
                Address = request.Address,
                Description = request.Description,
            });
            await _orderHubService.OrderAddedMessageAsync("Sipariş oluşturuldu");
            return new();
        }
    }
}
