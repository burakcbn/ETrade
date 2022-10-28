using ETradeStudy.Application.Abstractions;
using ETradeStudy.Application.Consts;
using ETradeStudy.Application.CustomAttributes;
using ETradeStudy.Application.Enums;
using ETradeStudy.Application.Features.Commands.Product.CreateProduct;
using ETradeStudy.Application.Features.Commands.Product.RemoveProduct;
using ETradeStudy.Application.Features.Commands.Product.UpdateProduct;
using ETradeStudy.Application.Features.Queries.Product.GetAllProduct;
using ETradeStudy.Application.Features.Queries.Product.GetByIdProduct;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.RequestParams;
using ETradeStudy.Application.ViewModel;
using ETradeStudy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeStudy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        readonly ILogger<ProductsController> _logger;
        readonly IStorageService _storageService;
        readonly IProductImageFileWrite _productImageFileWrite;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger, IStorageService storageService, IProductImageFileWrite productImageFileWrite)
        {
            _mediator = mediator;
            _logger = logger;
            _storageService = storageService;
            _productImageFileWrite = productImageFileWrite;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> Add(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> upload([FromQuery] string id)
        //{

        //    var data = Request.Form.Files;
        //    List<(string filename, string pathorcontainername)> result = await _storageService.UploadAsync("resource/products-images", Request.Form.Files);
        //    await _productImageFileWrite.AddRangeAsync(result.Select(p => new ProductImageFile()
        //    {
        //        FileName = p.filename,
        //        Path = p.pathorcontainername,
        //        Storage = _storageService.StorageName,
        //    }).ToList());
        //    return Ok();
        //}

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

    }
}
