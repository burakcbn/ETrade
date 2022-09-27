using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<Product> Products { get; set; }
    }
}
