using ETradeStudy.Application.Repositories.CompletedOrder;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.CompletedOrder
{
    public class CompletedOrderWrite : WriteRepository<Domain.Entities.CompletedOrder>, ICompletedOrderWrite
    {
        public CompletedOrderWrite(ETradeStudyContext context) : base(context)
        {
        }
    }
}
