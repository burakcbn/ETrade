using AutoMapper;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Application.Exceptions;
using ETradeStudy.Application.Helpers;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AssignRoleToUserAsync(string id, string[] roles)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                var _roles = await _userManager.GetRolesAsync(appUser);
                IdentityResult result = await _userManager.RemoveFromRolesAsync(appUser, _roles);
                
                await _userManager.AddToRolesAsync(appUser, roles);
            }
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            AppUser appUser = _mapper.Map<AppUser>(model);
            appUser.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (response.Succeeded)
            {
                response.Message = "Kullanıcı kaydı başarıyla gerçekleştirildi";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}";
                }
            }
            return response;

        }

        public ListUserDto GetAllUsers(int page, int size)
        {
            var query = _userManager.Users;
            var users = query.Skip(page * size).Take(size).ToList();
            return new()
            {
                Users = users.Select(user => new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    NameSurname = user.NameSurname,
                    TwoFactorEnabled = user.TwoFactorEnabled
                }).ToList(),
                Count = query.Count()
            };
        }

        public async Task<List<string>> GetRolesToUserAsync(string id)
        {
           AppUser appUser= await _userManager.FindByIdAsync(id);
            if (appUser!=null)
            {
              var userRoles=await _userManager.GetRolesAsync(appUser);
                return userRoles.ToList(); 
            }
            return new();
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(appUser, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(appUser);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate)
        {

            if (appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(appUser);
            }
            else
                throw new NotFoundUserException();
        }
    }
}
