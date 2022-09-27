using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Stock { get; set; }
        public long Price { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
