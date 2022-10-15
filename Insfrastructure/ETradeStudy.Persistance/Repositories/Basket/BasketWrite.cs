using ETradeStudy.Application.Repositories.Basket;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.Basket
{
    public class BasketWrite : WriteRepository<Domain.Entities.Basket>, IBasketWrite
    {
        public BasketWrite(ETradeStudyContext context) : base(context)
        {
        }
    }
}
