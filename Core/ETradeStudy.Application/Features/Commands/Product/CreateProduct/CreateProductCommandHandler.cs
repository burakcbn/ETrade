
using ETradeStudy.Application.Abstractions.Hubs;
using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p = ETradeStudy.Domain.Entities.Product;

namespace ETradeStudy.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWrite _productWrite;
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWrite productWrite, IProductHubService productHubService)
        {
            _productWrite = productWrite;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWrite.AddAsync(new() { ProductName = request.ProductName, Price = request.Price, Stock = request.Stock,CategoryId=Guid.Parse(request.CategoryId) });
            await _productWrite.SaveAsync();
            await _productHubService.ProductAddedMessageAsync("Ürün eklendi");
            return new();
        }
    }
}
