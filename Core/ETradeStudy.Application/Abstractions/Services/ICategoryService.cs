using ETradeStudy.Application.DTOs.Category;
using ETradeStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories();
        Task<CategoryDto> GetCategoryByIdAsync(string id);
        Task AddAsync(CreateCategoryDto categoryDto);

        Task<List<CategoryDtos>> CategoriesWithProductQuantitiesAsync();
    }
}
