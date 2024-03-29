﻿using ETradeStudy.Application.Features.Commands.AppUser.LoginUser;
using ETradeStudy.Application.Features.Commands.AppUser.PasswordReset;
using ETradeStudy.Application.Features.Commands.AppUser.RefreshTokenLogin;
using ETradeStudy.Application.Features.Commands.AppUser.VerifyResetToken;
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
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshToken)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshToken);
            return Ok(response);
        }
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }

    }
}
