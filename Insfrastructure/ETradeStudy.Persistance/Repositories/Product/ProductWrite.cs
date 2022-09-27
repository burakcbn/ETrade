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
    public class ProductWrite : WriteRepository<Product>, IProductWrite
    {
        public ProductWrite(ETradeStudyContext context) : base(context)
        {
        }
    }
}
