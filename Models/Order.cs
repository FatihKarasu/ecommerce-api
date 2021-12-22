using System;

namespace ecommerceApi
{
    public class Order
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string DeliveryAddressId { get; set; }
        public string BillingAddressId { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }

    }
}
