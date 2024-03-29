﻿using ETradeStudy.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
        Task<bool> ProductUpdateWithQRCodeAsync(string productId, int stock);
        Task<(List<ProductDto>, int)> GetByCategoryIdProductAsync(string categoryId);
    }
}
