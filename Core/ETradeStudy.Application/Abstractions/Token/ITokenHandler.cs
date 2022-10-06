using ETradeStudy.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token CreateAcessToken(int seconds,AppUser appUser);
        string CreateRefreshToken();
    }
}
