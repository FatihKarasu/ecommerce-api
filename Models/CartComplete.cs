using System;

namespace ecommerceApi
{
    public class CartComplete
    {
        public string CartItemId { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string ProductDetail { get; set; }
        
        public string ProductPrice { get; set; }

        public string ProductImage { get; set; }

        public int Amount { get; set; }
        

    }
}
