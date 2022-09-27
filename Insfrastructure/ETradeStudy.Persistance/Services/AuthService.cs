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

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password,int accessTokenLifeTime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (appUser == null)
                throw new NotFoundUserException("Kullanıcı adı veya email hatalı");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (result.Succeeded)
                return _tokenHandler.CreateAcessToken(accessTokenLifeTime);
            
            throw new AuthenticationErrorException(); 
        }
    }
}
