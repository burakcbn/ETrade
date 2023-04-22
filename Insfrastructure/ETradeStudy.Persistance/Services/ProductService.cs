using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Product;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ETradeStudy.Percistance.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRead _productRead;
        private readonly IProductWrite _productWrite;
        private readonly IQRCodeService _qRCodeService;

        public ProductService(IProductRead productRead, IQRCodeService qRCodeService, IProductWrite productWrite)
        {
            _productRead = productRead;
            _qRCodeService = qRCodeService;
            _productWrite = productWrite;
        }

        public async Task<(List<ProductDto>, int)> GetByCategoryIdProductAsync(string categoryId)
        {
            if (categoryId == null)
                return (null, 0);

            var query = _productRead.GetWhere(x => x.CategoryId == Guid.Parse(categoryId))
                .Select(x => new ProductDto
                {
                    Id = x.Id.ToString(),
                    CategoryId = x.CategoryId.ToString(),
                    Price = x.Price,
                    ProductName = x.ProductName,
                    Stock = x.Stock,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdateDate
                });
            var count = query.Count();
            var products = await query.ToListAsync();

            return (products, count);
        }

        public async Task<bool> ProductUpdateWithQRCodeAsync(string productId, int stock)
        {
            Product product = await _productRead.GetByIdAsync(productId);
            if (product != null)
            {
                product.Stock = stock;
                _productWrite.Update(product);
                await _productWrite.SaveAsync();
                return true;
            }
            return false;
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
