using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.DTOs.Configuration
{
    public class Menu
    {
        public string Name{ get; set; }
        public List<Action> Actions { get; set; }=new();
    }
}
