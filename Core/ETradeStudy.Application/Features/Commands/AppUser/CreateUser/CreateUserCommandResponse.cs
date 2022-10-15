using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
