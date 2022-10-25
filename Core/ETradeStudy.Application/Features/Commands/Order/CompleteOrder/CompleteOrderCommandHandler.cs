using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool state, CompletedOrderDto completedOrder) = await _orderService.CompleteOrderAsync(request.Id);
            if (state)
                await _mailService.SendCompletedOrderMailAsync(completedOrder.Email, completedOrder.OrderCode, completedOrder.OrderDate, completedOrder.UserName);
            return new();
        }
    }
}
