using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Abstractions.Token;
using ETradeStudy.Application.DTOs;
using ETradeStudy.Application.Exceptions;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (appUser == null)
                throw new NotFoundUserException("Kullanıcı adı veya email hatalı");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAcessToken(accessTokenLifeTime);
                await _userService.UpdateRefreshToken(token.RefreshToken, appUser, token.Expration, 15);
                return token;
            }
            else
                throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? appUser = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (appUser != null && appUser?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAcessToken(15);
                await _userService.UpdateRefreshToken(token.RefreshToken, appUser, token.Expration, 15);
                return token;
            }
            else
                throw new NotFoundUserException();

        }
    }
}
