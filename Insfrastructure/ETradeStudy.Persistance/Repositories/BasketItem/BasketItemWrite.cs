using ETradeStudy.Application.Repositories.BasketItem;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.BasketItem
{
    public class BasketItemWrite : WriteRepository<Domain.Entities.BasketItem>, IBasketItemWrite
    {
        public BasketItemWrite(ETradeStudyContext context) : base(context)
        {
        }
    }
}
