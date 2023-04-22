using MediatR;

namespace ETradeStudy.Application.Features.Queries.Category.GetByIdCategory
{
    public class GetByCategoryIdQueryRequest:IRequest<GetByCategoryIdQueryResponse>
    {
        public string CategoryId { get; set; }
    }
}