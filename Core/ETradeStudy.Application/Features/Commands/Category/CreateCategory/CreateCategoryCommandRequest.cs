using ETradeStudy.Application.DTOs.Category;
using MediatR;

namespace ETradeStudy.Application.Features.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandRequest:IRequest<bool>
    {
        public string CategoryName{ get; set; }
    }
}