using AutoMapper;
using ETradeStudy.Application.Features.AppUser.Commands.LoginUser;
using ETradeStudy.Application.Features.AppUser.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeStudy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {            
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenLoginCommandRequest refreshToken)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshToken);
            return Ok(response);
        }

    }
}
