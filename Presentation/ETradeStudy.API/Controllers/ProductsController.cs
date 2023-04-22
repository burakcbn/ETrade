using ETradeStudy.Application.Abstractions;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Consts;
using ETradeStudy.Application.CustomAttributes;
using ETradeStudy.Application.Enums;
using ETradeStudy.Application.Features.Commands.Product.CreateProduct;
using ETradeStudy.Application.Features.Commands.Product.ProductUpdateWithQRCode;
using ETradeStudy.Application.Features.Commands.Product.RemoveProduct;
using ETradeStudy.Application.Features.Commands.Product.UpdateProduct;
using ETradeStudy.Application.Features.Queries.Category.GetByIdCategory;
using ETradeStudy.Application.Features.Queries.Product.GetAllProduct;
using ETradeStudy.Application.Features.Queries.Product.GetByCategoryProduct;
using ETradeStudy.Application.Features.Queries.Product.GetByIdProduct;
using ETradeStudy.Application.Features.Queries.Product.GeyByDynamic;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.Dynamic;
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
        private readonly IProductService _productService;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger, IStorageService storageService, IProductImageFileWrite productImageFileWrite, IProductService productService)
        {
            _mediator = mediator;
            _logger = logger;
            _storageService = storageService;
            _productImageFileWrite = productImageFileWrite;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpPost("deneme")]
        public async Task<IActionResult> GetByDynamic(GetByDynamicQueryRequest getByDynamicQueryRequest)
        {
           var response= await _mediator.Send(getByDynamicQueryRequest);
            return Ok(response);
        }
    

        [HttpGet("get-by-categoryId/{CategoryId}")]
        public async Task<IActionResult> GetByCategoryIdProduct([FromRoute] GetByCategoryIdProductQueryRequest getByCategoryIdProductQueryRequest)
        {
            GetByCategoryIdProductQueryResponse response = await _mediator.Send(getByCategoryIdProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("qrcode/{productId}")]
        public async Task<IActionResult> GetQRCodeToProduct([FromRoute] string productId)
        {
            var responseByte = await _productService.QRCodeToProductAsync(productId);
            return File(responseByte, "image/png ");
        }
        [HttpPut("qrcode-stock-update")]
        public async Task<IActionResult> ProductUpdateWithQRCode(ProductUpdateWithQRCodeCommandRequest productUpdateWithQRCodeCommandRequest)
        {
            ProductUpdateWithQRCodeCommandResponse response = await _mediator.Send(productUpdateWithQRCodeCommandRequest);
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
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
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

     

    }
}
