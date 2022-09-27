using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Createuser = ETradeStudy.Application.DTOs.User.CreateUser;
namespace ETradeStudy.Application.Features.AppUser.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        //public Createuser Createuser { get; set; }
    }
}
