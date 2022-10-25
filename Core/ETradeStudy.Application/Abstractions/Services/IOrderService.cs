using ETradeStudy.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<ListOrderDto> GetAllOrdersAsync(int page,int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id);
        Task CreateOrderAsync(CreateOrderDto createOrder);
    }
}
