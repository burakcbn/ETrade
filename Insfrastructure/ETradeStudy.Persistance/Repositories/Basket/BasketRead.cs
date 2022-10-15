using ETradeStudy.Application.Repositories;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.Basket
{
    public class BasketRead : ReadRepository<Domain.Entities.Basket>, IBasketRead
    {
        public BasketRead(ETradeStudyContext context) : base(context)
        {
        }
    }
}
