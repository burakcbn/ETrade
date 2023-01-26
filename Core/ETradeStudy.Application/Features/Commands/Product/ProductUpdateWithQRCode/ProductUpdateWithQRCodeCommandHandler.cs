using ETradeStudy.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.Product.ProductUpdateWithQRCode
{
    public class ProductUpdateWithQRCodeCommandHandler : IRequestHandler<ProductUpdateWithQRCodeCommandRequest, ProductUpdateWithQRCodeCommandResponse>
    {
        private readonly IProductService _productService;

        public ProductUpdateWithQRCodeCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductUpdateWithQRCodeCommandResponse> Handle(ProductUpdateWithQRCodeCommandRequest request, CancellationToken cancellationToken)
        {
            return new() { Succeeded= await _productService.ProductUpdateWithQRCodeAsync(request.ProductId, request.Stock)};
        }
    }
}
