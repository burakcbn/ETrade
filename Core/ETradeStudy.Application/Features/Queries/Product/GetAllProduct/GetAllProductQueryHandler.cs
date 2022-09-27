using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductRead _productRead;

        public GetAllProductQueryHandler(IProductRead productRead)
        {
            _productRead = productRead;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var count = _productRead.GetAll(false).Count();
            var products = _productRead.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new { p.Id, p.Price, p.Stock, p.CreatedDate, p.UpdateDate });
            return new()
            {
                Count = count,
                Products = products
            };
        }
    }
}
