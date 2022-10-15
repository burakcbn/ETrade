using MediatR;

namespace ETradeStudy.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryRequest:IRequest<List<GetBasketItemsQueryResponse>>
    {
    }
}