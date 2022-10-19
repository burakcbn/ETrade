using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Order;
using ETradeStudy.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWrite _orderWrite;

        public OrderService(IOrderWrite orderWrite)
        {
            _orderWrite = orderWrite;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrder)
        {
           await _orderWrite.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
            });
            await _orderWrite.SaveAsync();
        }
    }
}
