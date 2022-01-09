using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ecommerceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public Order Get(string orderId)
        {
            Order _order = new Order();

            foreach (Order order in orderList)
            {
                if (order.OrderId == orderId)
                {
                    _order = order;
                }

            }

            return _order;
        }
        [HttpGet("user/{userId}")]
        public List<OrderComplete> GetById(string userId)
        {
            List<OrderComplete> orders = new List<OrderComplete>();

            foreach (Order order in orderList)
            {

                if (order.UserId == userId)
                {
                    OrderComplete orderComplete = new OrderComplete();
                    orderComplete.OrderProducts = new List<OrderProduct>();

                    foreach (OrderProduct op in orderProducts)
                    {
                        if (op.OrderId == order.OrderId)
                        {
                            orderComplete.OrderProducts.Add(op);
                        }
                    }
                    orderComplete.Order = order;
                    orders.Add(orderComplete);
                }

            }

            return orders.OrderByDescending(o=>int.Parse(o.Order.OrderId)).ToList();
        }
       
        [Authorize]
        [HttpPost]
        public Order AddOrder([FromForm] Order order)
        {
            order.OrderId = (orderList.Count + 1).ToString();
            order.OrderStatus = "Pending";
            order.PaymentMethod = "Credit Card";
            order.OrderDate = DateTime.Now.ToString();
            List<CartItem> cartItems =new List<CartItem>();

            foreach (CartItem cartItem in CartController.cartItemList)
            {
                if (cartItem.UserId == order.UserId)
                {
                    foreach (Product product in ProductsController.productsList)
                    {
                        if (product.ProductId == cartItem.ProductId)
                        {
                            OrderProduct orderProduct = new OrderProduct();
                            orderProduct.Product = new Product();
                            orderProduct.OrderProductId = (orderProducts.Count + 1).ToString();
                            orderProduct.OrderId = order.OrderId;
                            orderProduct.Product = product;
                            foreach (Color color in ProductsController.Colors)
                            {
                                if (color.ColorId == cartItem.ColorId)
                                {
                                    orderProduct.ProductColor = color;
                                    break;
                                }
                            }
                            foreach (Size size in ProductsController.Sizes)
                            {
                                if (size.SizeId == cartItem.SizeId)
                                {
                                    orderProduct.ProductSize = size;
                                    break;
                                }
                            }
                            orderProduct.Amount = cartItem.Amount;
                            orderProducts.Add(orderProduct);
                            break;
                        }

                    }
                   cartItems.Add(cartItem);
                }

            }
            foreach (CartItem item in cartItems)
            {
                CartController.cartItemList.Remove(item);
            }
            orderList.Add(order);
            return order;
        }

        public static List<Order> orderList = new List<Order>() { };

        public static List<OrderProduct> orderProducts = new List<OrderProduct>() { };
    }
}
