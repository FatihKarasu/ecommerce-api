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
            Order _order=new Order();

            foreach (Order order in orderList)
            {
                if(order.OrderId==orderId)
                {
                    _order=order;
                }
                
            }
            
            return _order;
        }
        [Authorize]
        [HttpGet("user/{userId}")]
        public List<Order> GetById(string userId)
        {
            List<Order> orders=new List<Order>();

            foreach (Order order in orderList)
            {
                if(order.UserId==userId)
                {
                    orders.Add(order);
                }
                
            }
            
            return orders;
        }
        [Authorize]
        [HttpPost("addorder")]
        public Response AddAddress([FromForm] Order order)
        {
            Response response = new Response();
            //Get the last item's ID and add 1.
            order.OrderId=(orderList.Count()+1).ToString();
            orderList.Add(order);
            return response;
        }
        
         public List<Order> orderList = new List<Order>(){
            new Order(){OrderId="1",UserId="1",ProductId="1",ProductTitle="Product 1 Title",ProductDetail="Product 1 Detail",ProductPrice="123",ProductImage="../Images/1.jpg",OrderDate="Order Date",OrderStatus="Delivered"},
            new Order(){OrderId="2",UserId="1",ProductId="2",ProductTitle="Product 2 Title",ProductDetail="Product 2 Detail",ProductPrice="1234",ProductImage="../Images/2.jpg",OrderDate="Order Date",OrderStatus="Pending"},
            new Order(){OrderId="3",UserId="1",ProductId="3",ProductTitle="Product 3 Title",ProductDetail="Product 3 Detail",ProductPrice="12345",ProductImage="../Images/3.jpg",OrderDate="Order Date",OrderStatus="Cancelled"},
           };
    }
}
