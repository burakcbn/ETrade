using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Order;
using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            SingleOrder singleOrder = await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                Id = singleOrder.Id,
                Address = singleOrder.Address,
                BasketItems = singleOrder.BasketItems,
                CreatedDate = singleOrder.CreatedDate,
                Description = singleOrder.Description,
                OrderCode = singleOrder.OrderCode,
            };
        }
    }
}
