using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Product.GetByCategoryProduct
{
    public class GetByCategoryIdProductQueryHandler : IRequestHandler<GetByCategoryIdProductQueryRequest, GetByCategoryIdProductQueryResponse>
    {
        private readonly IProductService _productService;

        public GetByCategoryIdProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetByCategoryIdProductQueryResponse> Handle(GetByCategoryIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            (List<ProductDto> data, int count) = await _productService.GetByCategoryIdProductAsync(request.CategoryId);
            return new() { Products = data, Count = count };
        }
    }
}
