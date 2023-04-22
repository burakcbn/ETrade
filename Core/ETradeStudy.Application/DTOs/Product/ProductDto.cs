using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.DTOs.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public int  Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
