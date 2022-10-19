using ETradeStudy.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public  interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrder);
    }
}
