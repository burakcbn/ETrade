using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.Category;
using ETradeStudy.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRead _categoryRead;
        private readonly ICategoryWrite _categoryWrite;

        public CategoryService(ICategoryRead categoryRead, ICategoryWrite categoryWrite)
        {
            _categoryRead = categoryRead;
            _categoryWrite = categoryWrite;
        }

        public async Task AddAsync(CreateCategoryDto categoryDto)
        {
            await _categoryWrite.AddAsync(new()
            {
                CategoryName = categoryDto.CategoryName,

            });
            await _categoryWrite.SaveAsync();
        }

        public async Task<List<CategoryDtos>> CategoriesWithProductQuantitiesAsync()
        {
            var query = _categoryRead.Table
                .Include(c => c.Products);

            return await query.Select(x => new CategoryDtos()
            {
                Id = x.Id.ToString(),
                CategoryName = x.CategoryName,
                ProductCount = x.Products.Count,
            }).ToListAsync();
        
        
        }

        public List<CategoryDto> GetAllCategories()
        {
            return _categoryRead.GetAll(false).Select(x => new CategoryDto()
            {
                CategoryId = x.Id.ToString(),
                CategoryName = x.CategoryName,
            }).ToList();

        }

        public async Task<CategoryDto> GetCategoryByIdAsync(string id)
        {
            var category = await _categoryRead.GetSingleAsync(x => x.Id == Guid.Parse(id));
            return new CategoryDto() { CategoryId = category.Id.ToString(), CategoryName = category.CategoryName };

        }
    }
}
