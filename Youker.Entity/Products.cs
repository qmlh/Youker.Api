using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class Products
    {
        public int product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public decimal unit_price { get; set; }
        public int quantity { get; set; }

    }
}
