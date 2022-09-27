using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductRead _productRead;
        private readonly IProductWrite _productWrite;

        public UpdateProductCommandHandler(IProductRead productRead, IProductWrite productWrite)
        {
            _productRead = productRead;
            _productWrite = productWrite;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRead.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Price = request.Price;
            _productWrite.Update(product);
            await _productWrite.SaveAsync();
            return new();
        }
    }
}
