using AutoMapper;
using ETradeStudy.Application.Consts;
using ETradeStudy.Application.CustomAttributes;
using ETradeStudy.Application.Enums;
using ETradeStudy.Application.Features.Commands.Supplier.CreateSupplier;
using ETradeStudy.Application.Features.Queries.Supplier.GetAllSupplier;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeStudy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator mediator;

        public SuppliersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] GetAllSupplierQueryRequest getAllSupplierQueryRequest)
        {
            var response = await mediator.Send(getAllSupplierQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        //[Authorize]
        //[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Suppliers, ActionType = ActionType.Writing, Definition = "Create Supplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommandRequest createSupplierCommandRequest )
        {
            var response = await mediator.Send(createSupplierCommandRequest);
            return Ok(response);
        }
    }
}
