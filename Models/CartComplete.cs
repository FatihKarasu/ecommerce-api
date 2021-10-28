using System;

namespace ecommerceApi
{
    public class CartComplete
    {
        public string CartItemId { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int Amount { get; set; }
    }
}
