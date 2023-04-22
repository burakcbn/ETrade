using ETradeStudy.Application.Repositories.Dynamic;
using MediatR;

namespace ETradeStudy.Application.Features.Queries.Product.GeyByDynamic
{
    public class GetByDynamicQueryRequest:IRequest<GetByDynamicQueryResponse>
    {
        public Dynamic Dynamic { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}