using ETradeStudy.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllProductQueryHandler> _logger;

        public GetAllProductQueryHandler(IProductRead productRead, ILogger<GetAllProductQueryHandler> logger)
        {
            _productRead = productRead;
            _logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all products");
            var count = _productRead.GetAll(false).Count();
            var products = _productRead.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).OrderBy(p => p.ProductName).Select(p => new { p.Id,p.ProductName, p.Price, p.Stock, p.CreatedDate, p.UpdateDate,p.CategoryId });
            
            return new()
            {
                Count = count,
                Products = products
            };
        }
    }
}
