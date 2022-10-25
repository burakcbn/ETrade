using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Order;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.CompletedOrder;
using ETradeStudy.Domain.Entities;
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
        private readonly ICompletedOrderWrite _completedOrderWrite;
        private readonly ICompletedOrderRead _completedOrderRead;

        public OrderService(IOrderWrite orderWrite, IOrderRead orderRead, ICompletedOrderWrite completedOrderWrite, ICompletedOrderRead completedOrderRead)
        {
            _orderWrite = orderWrite;
            _orderRead = orderRead;
            _completedOrderWrite = completedOrderWrite;
            _completedOrderRead = completedOrderRead;
        }

        public async Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id)
        {
            Order? order = await _orderRead.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User).FirstOrDefaultAsync(o=>o.Id==Guid.Parse(id));
            if (order != null)
            {
                await _completedOrderWrite.AddAsync(new()
                {
                    OrderId = Guid.Parse(id)
                });
                return (await _completedOrderWrite.SaveAsync() > 0, new()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CreatedDate,
                    UserName = order.Basket.User.UserName,
                    Email= order.Basket.User.Email,
                });
            }
            return (false,null);
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
            var query = _orderRead.Table.Include(o => o.Basket)
                   .ThenInclude(b => b.User)
                   .Include(o => o.Basket)
                      .ThenInclude(b => b.BasketItems)
                      .ThenInclude(bi => bi.Product);



            var data = query.Skip(page * size).Take(size);
            /*.Take((page * size)..size);*/

            var data2 = from order in data
                        join completedOrder in _completedOrderRead.Table
                           on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false
                        };

            return new()
            {
                Count = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };

            //Take((page*size)..size)
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var query = await _orderRead.Table
                .Include(o => o.Basket)
                    .ThenInclude(o => o.BasketItems)
                        .ThenInclude(bi => bi.Product)
                            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if (query != null)
            {
                return new SingleOrder()
                {
                    Id = query.Id.ToString(),
                    Address = query.Address,
                    BasketItems = query.Basket.BasketItems.Select(bi => new
                    {
                        bi.Product.ProductName,
                        bi.Product.Price,
                        bi.Quantity
                    }),
                    CreatedDate = query.CreatedDate,
                    Description = query.Description,
                    OrderCode = query.OrderCode,
                };
            }
            return new();
        }
    }
}
