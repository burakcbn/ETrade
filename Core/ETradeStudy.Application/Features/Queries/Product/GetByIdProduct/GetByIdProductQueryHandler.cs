using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ETradeStudy.Domain.Entities;
namespace ETradeStudy.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductRead _productRead;
        public GetByIdProductQueryHandler(IProductRead productRead)
        {
            _productRead = productRead;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productRead.GetByIdAsync(request.Id);
            return new()
            {
                ProductName = product.ProductName,
                CategoryId = product.CategoryId.ToString(),
                Price = product.Price,
                Stock = product.Stock,
            };
        }
    }
}
