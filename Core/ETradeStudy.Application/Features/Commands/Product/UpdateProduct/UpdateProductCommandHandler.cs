using ETradeStudy.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductRead _productRead;
        private readonly IProductWrite _productWrite;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRead productRead, IProductWrite productWrite, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRead = productRead;
            _productWrite = productWrite;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRead.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.CategoryId = Guid.Parse(request.CategoryId);
            product.ProductName= request.ProductName;
            product.Price = request.Price;
            _productWrite.Update(product);
            await _productWrite.SaveAsync();
            _logger.LogInformation("Product Güncellendi...");
            return new();
        }
    }
}
