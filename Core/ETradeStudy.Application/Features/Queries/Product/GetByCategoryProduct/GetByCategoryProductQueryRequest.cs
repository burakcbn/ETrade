using MediatR;

namespace ETradeStudy.Application.Features.Queries.Product.GetByCategoryProduct
{
    public class GetByCategoryIdProductQueryRequest:IRequest<GetByCategoryIdProductQueryResponse>
    {
        public string CategoryId { get; set; }
    }
}