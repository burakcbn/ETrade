using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Domain.Entities;
using System.Text.Json;

namespace ETradeStudy.Percistance.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRead _productRead;
        private readonly IQRCodeService _qRCodeService;

        public ProductService(IProductRead productRead, IQRCodeService qRCodeService)
        {
            _productRead = productRead;
            _qRCodeService = qRCodeService;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            Product product = await _productRead.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found");
            var plainObject = new
            {
                product.Id,
                product.ProductName,
                product.Price,
                product.Stock,
                product.CreatedDate,
            };
            var plainText = JsonSerializer.Serialize(plainObject);
            return _qRCodeService.GenerateQRCode(plainText);
        }
    }
}
