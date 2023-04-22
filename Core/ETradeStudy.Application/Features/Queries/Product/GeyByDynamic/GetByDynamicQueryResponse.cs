using ETradeStudy.Application.DTOs.Product;

namespace ETradeStudy.Application.Features.Queries.Product.GeyByDynamic
{
    public class GetByDynamicQueryResponse
    {
        public int Count { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}