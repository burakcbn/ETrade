using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.BasketItem;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.BasketItem
{
    public class BasketItemRead : ReadRepository<Domain.Entities.BasketItem>, IBasketItemRead
    {
        public BasketItemRead(ETradeStudyContext context) : base(context)
        {
        }
    }
}
