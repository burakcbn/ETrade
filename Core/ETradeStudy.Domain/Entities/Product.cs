﻿using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        //public ICollection<Order> Orders { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
