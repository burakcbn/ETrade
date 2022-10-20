using ETradeStudy.Application.DTOs.Order;

namespace ETradeStudy.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryResponse
    {
        public object Orders{ get; set; }
        public int Count { get; set; }

    }
}