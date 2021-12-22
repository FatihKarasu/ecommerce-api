using System;

namespace ecommerceApi
{
    public class OrderProduct
    {
        public string OrderProductId { get; set; }
        public string OrderId { get; set; }
        public Product Product {get;set;}
        public Size ProductSize { get; set; }
        public Color ProductColor { get; set; }
        public int Amount { get; set; }


    }
}
