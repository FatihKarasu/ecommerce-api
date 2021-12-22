using System;
using System.Collections.Generic;

namespace ecommerceApi
{
    public class OrderComplete
    {
        public Order Order { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
