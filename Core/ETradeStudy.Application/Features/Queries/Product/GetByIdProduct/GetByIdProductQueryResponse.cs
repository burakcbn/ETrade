﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETradeStudy.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
    }
}
