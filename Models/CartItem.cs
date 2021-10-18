using System;

namespace ecommerceApi
{
    public class CartItem
    {
        public string CartItemId { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public int Amount { get; set; }

    }
}
