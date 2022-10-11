using ETradeStudy.Application.Abstractions;
using ETradeStudy.Application.Abstractions.Storage;
using ETradeStudy.Application.Features.Product.Commands.CreateProduct;
using ETradeStudy.Application.Features.Product.Commands.RemoveProduct;
using ETradeStudy.Application.Features.Product.Commands.UpdateProduct;
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

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Add(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload([FromQuery] string id)
        //{
        //    var data = Request.Form.Files;
        //    List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("resource/products-images", Request.Form.Files);
        //    await _productImageFileWrite.AddRangeAsync(result.Select(p => new ProductImageFile()
        //    {
        //        FileName = p.fileName,
        //        Path = p.pathOrContainerName,
        //        Storage = _storageService.StorageName,
        //    }).ToList());
        //    return Ok();
        //}

        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

    }
}
