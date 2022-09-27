
using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWrite _productWrite;

        public CreateProductCommandHandler(IProductWrite productWrite)
        {
            _productWrite = productWrite;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            await _productWrite.AddAsync(new() { Price = request.Price, Stock = request.Stock });
            await _productWrite.SaveAsync();
            return new();
        }
    }
}
