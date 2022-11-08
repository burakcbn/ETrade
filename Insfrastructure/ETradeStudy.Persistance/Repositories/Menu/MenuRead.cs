using ETradeStudy.Application.Repositories;
using ETradeStudy.Domain.Entities;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories
{
    public class MenuRead : ReadRepository<Menu>, IMenuRead
    {
        public MenuRead(ETradeStudyContext context) : base(context)
        {
        }
    }
}
