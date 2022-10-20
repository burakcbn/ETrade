using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Order;
using ETradeStudy.Application.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private readonly IOrderRead _orderRead;

        public OrderService(IOrderWrite orderWrite, IOrderRead orderRead)
        {
            _orderWrite = orderWrite;
            _orderRead = orderRead;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrder)
        {

            await _orderWrite.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = new Random().NextDouble().ToString().Substring(5)
            });
            await _orderWrite.SaveAsync();
        }

        public async Task<ListOrderDto> GetAllOrdersAsync(int page, int size)
        {
            var result = _orderRead.Table.
                   Include(o => o.Basket).
                   ThenInclude(b => b.User).
                   Include(o => o.Basket).
                   ThenInclude(o => o.BasketItems).
                   ThenInclude(o => o.Product);
            var data = result.Skip(page * size).Take(size);
            return new()
            {
                Count = await result.CountAsync(),
                Orders = data.Select(o => new
                {
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName=o.Basket.User.UserName,
                })
            };

        //Take((page*size)..size)
        }
    }
}
