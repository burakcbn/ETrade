using AutoMapper;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.DTOs.User;
using ETradeStudy.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            AppUser appUser = _mapper.Map<AppUser>(model);
            appUser.Id =  Guid.NewGuid().ToString();
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
    }
}
