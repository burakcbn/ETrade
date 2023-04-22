using ETradeStudy.Application.Abstractions.Services;
using MediatR;

namespace ETradeStudy.Application.Features.Queries.Category.GetByIdCategory
{
    public class GetByCategoryIdQueryHandler : IRequestHandler<GetByCategoryIdQueryRequest, GetByCategoryIdQueryResponse>
    {
        private readonly ICategoryService _categoryService;

        public GetByCategoryIdQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetByCategoryIdQueryResponse> Handle(GetByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            return new() { CategoryDto = await _categoryService.GetCategoryByIdAsync(request.CategoryId) };
        }
    }
}
