using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser appUser , DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken,string newPassword);
        ListUserDto GetAllUsers(int page,int size);
        Task AssignRoleToUserAsync(string id, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string id);
    }
}
