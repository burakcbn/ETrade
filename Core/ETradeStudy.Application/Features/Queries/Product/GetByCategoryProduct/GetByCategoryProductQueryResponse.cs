using ETradeStudy.Application.DTOs.Product;

namespace ETradeStudy.Application.Features.Queries.Product.GetByCategoryProduct
{
    public class GetByCategoryIdProductQueryResponse
    {
        public List<ProductDto> Products { get; set; }
        public int Count { get; set; }
    }
}