using ETradeStudy.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Category.CategoriesWithProductQuantities
{
    public class CategoriesWithProductQuantitiesQueryHandler : IRequestHandler<CategoriesWithProductQuantitiesQueryRequest, CategoriesWithProductQuantitiesQueryResponse>
    {
        private readonly ICategoryService _categoryService;

        public CategoriesWithProductQuantitiesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoriesWithProductQuantitiesQueryResponse> Handle(CategoriesWithProductQuantitiesQueryRequest request, CancellationToken cancellationToken)
        {
            return new() { CategoryDtos = await _categoryService.CategoriesWithProductQuantitiesAsync() };
        }
    }
}
