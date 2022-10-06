using ETradeStudy.Application.Abstractions.Token;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ETradeStudy.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAcessToken(int seconds, AppUser appuser)
        {
            Application.DTOs.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expration = DateTime.UtcNow.AddSeconds(seconds);

            JwtSecurityToken jwtSecurityToken =
            new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: new List<Claim> { new(ClaimTypes.Name, appuser.UserName) }
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            //string refreshToken = CreateRefreshToken();
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
