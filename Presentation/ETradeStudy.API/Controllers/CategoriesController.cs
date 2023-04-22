using ETradeStudy.Application.Consts;
using ETradeStudy.Application.CustomAttributes;
using ETradeStudy.Application.Features.Commands.Category.CreateCategory;
using ETradeStudy.Application.Features.Queries.Category.CategoriesWithProductQuantities;
using ETradeStudy.Application.Features.Queries.Category.GetAllCategory;
using ETradeStudy.Application.Features.Queries.Category.GetByIdCategory;
using ETradeStudy.Application.Features.Queries.Product.GeyByDynamic;
using ETradeStudy.Application.Repositories.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeStudy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoryQueryRequest getAllCategoryQueryRequest)
        {
            var result = await _mediator.Send(getAllCategoryQueryRequest);
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoriesWithProductQuantitiesQueryRequest categoriesWithProductQuantitiesQueryRequest)
        {
            var result = await _mediator.Send(categoriesWithProductQuantitiesQueryRequest);
            return Ok(result);
        }


        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetByIdCategory([FromRoute] GetByCategoryIdQueryRequest getByIdCategoryQueryRequest)
        {
            var result = await _mediator.Send(getByIdCategoryQueryRequest);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Categories, ActionType = Application.Enums.ActionType.Writing, Definition = "CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            var result = await _mediator.Send(createCategoryCommandRequest);
            return Ok(result);
        }
    }
}
