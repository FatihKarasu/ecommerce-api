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
    public class CartController : ControllerBase
    {
    
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<CartItem> Get()
        {
            
            
            return cartItemList;
        }
       
        [HttpGet("user/{userId}")]
        public List<CartComplete> GetById(string userId)
        {
            List<CartComplete> items=new List<CartComplete>();
            
            foreach (CartItem item in cartItemList)
            {
                if(item.UserId==userId)
                {
                    foreach (Product product in productsList)
                    {
                        if(item.ProductId==product.ProductId)
                        {
                            CartComplete complete = new CartComplete();
                            complete.CartItemId=item.CartItemId;
                            complete.UserId=item.UserId;
                            complete.ProductId=product.ProductId;
                            complete.ProductTitle=product.ProductTitle;
                            complete.ProductDetail=product.ProductDetail;
                            complete.ProductPrice=product.ProductPrice;
                            complete.ProductImage=product.ProductImage;
                            complete.Amount=item.Amount;
                            items.Add(complete);

                        }
                    }
                }
                
            }
            
            return items;
        }
        [Authorize]
        [HttpPost("add")]
        public CartComplete Add([FromForm] CartItem item)
        {
            CartComplete completeItem = new CartComplete();
            foreach (CartItem cartItem in cartItemList)
            {
                if(cartItem.UserId==item.UserId && cartItem.ProductId==item.ProductId)
                {
                    item.CartItemId=cartItem.CartItemId;
                    item.Amount+=cartItem.Amount;
                    cartItem.Amount=item.Amount;
                }
            }
            if(item.CartItemId==null)
            {
                if(cartItemList.Count!=0)
                {
                    item.CartItemId=(Int32.Parse(cartItemList.LastOrDefault().CartItemId)+1).ToString();
                    
                }
                else{
                    item.CartItemId="1";
                }
                cartItemList.Add(item);
            }
            foreach (Product product in productsList)
            {
                if(item.ProductId==product.ProductId)
                {
                    completeItem.CartItemId=item.CartItemId;
                    completeItem.UserId=item.UserId;
                    completeItem.ProductId=product.ProductId;
                    completeItem.ProductTitle=product.ProductTitle;
                    completeItem.ProductDetail=product.ProductDetail;
                    completeItem.ProductPrice=product.ProductPrice;
                    completeItem.ProductImage=product.ProductImage;
                    completeItem.Amount=item.Amount;

                }
            }

            return completeItem;
        }
        [Authorize]
        [HttpDelete("delete/{cartItemId}")]
        public Response DeleteCartItem(string cartItemId)
        {
            Response response = new Response();
            response.Message="Please try again.";
            response.Status="danger";
            CartItem item = cartItemList.SingleOrDefault(item => item.CartItemId == cartItemId);
            if (item != null)
            {
                cartItemList.Remove(item);
                response.Message="Item Deleted.";
                response.Status="success";
            }
            
            return response;
        }

        [Authorize]
        [HttpPost("changeamount")]
        public Response ChangeAmount([FromForm] CartItem item)
        {
            Response response = new Response();
            response.Message="Please try again.";
            response.Status="danger";
            foreach (CartItem cartItem in cartItemList)
            {
                if(cartItem.CartItemId==item.CartItemId)
                {
                    cartItem.Amount=item.Amount;
                    response.Message="";
                    response.Status="success";
                }
            }
            
            return response;
        }
        public static List<CartItem> cartItemList = new List<CartItem>(){
            new CartItem(){CartItemId="1",UserId="1",ProductId="1",Amount=2},
            new CartItem(){CartItemId="2",UserId="1",ProductId="2",Amount=1},
            new CartItem(){CartItemId="3",UserId="1",ProductId="3",Amount=3},
            new CartItem(){CartItemId="4",UserId="1",ProductId="4",Amount=5},
            new CartItem(){CartItemId="5",UserId="1",ProductId="5",Amount=4},
        };

        public static List<Product> productsList = new List<Product>(){
            new Product(){ProductId="1",ProductTitle="Product 1 Title",ProductDetail="Product 1 Detail",ProductPrice="123",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="2",ProductTitle="Product 2 Title",ProductDetail="Product 2 Detail",ProductPrice="324",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="3",ProductTitle="Product 3 Title",ProductDetail="Product 3 Detail",ProductPrice="34534",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="4",ProductTitle="Product 4 Title",ProductDetail="Product 4 Detail",ProductPrice="12312",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="5",ProductTitle="Product 5 Title",ProductDetail="Product 5 Detail",ProductPrice="534",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="6",ProductTitle="Product 6 Title",ProductDetail="Product 6 Detail",ProductPrice="5435",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="7",ProductTitle="Product 7 Title",ProductDetail="Product 7 Detail",ProductPrice="554",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="8",ProductTitle="Product 8 Title",ProductDetail="Product 8 Detail",ProductPrice="23",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="9",ProductTitle="Product 9 Title",ProductDetail="Product 9 Detail",ProductPrice="345",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="10",ProductTitle="Product 10 Title",ProductDetail="Product 10 Detail",ProductPrice="233",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="11",ProductTitle="Product 11 Title",ProductDetail="Product 11 Detail",ProductPrice="657",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="12",ProductTitle="Product 12 Title",ProductDetail="Product 12 Detail",ProductPrice="234",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="13",ProductTitle="Product 13 Title",ProductDetail="Product 13 Detail",ProductPrice="163",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="14",ProductTitle="Product 14 Title",ProductDetail="Product 14 Detail",ProductPrice="645",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="15",ProductTitle="Product 15 Title",ProductDetail="Product 15 Detail",ProductPrice="534",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="16",ProductTitle="Product 16 Title",ProductDetail="Product 16 Detail",ProductPrice="5435",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="17",ProductTitle="Product 17 Title",ProductDetail="Product 17 Detail",ProductPrice="554",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="18",ProductTitle="Product 18 Title",ProductDetail="Product 18 Detail",ProductPrice="23",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="19",ProductTitle="Product 19 Title",ProductDetail="Product 19 Detail",ProductPrice="345",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="20",ProductTitle="Product 20 Title",ProductDetail="Product 20 Detail",ProductPrice="233",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="21",ProductTitle="Product 21 Title",ProductDetail="Product 21 Detail",ProductPrice="657",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="22",ProductTitle="Product 22 Title",ProductDetail="Product 22 Detail",ProductPrice="234",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="23",ProductTitle="Product 23 Title",ProductDetail="Product 23 Detail",ProductPrice="163",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="24",ProductTitle="Product 24 Title",ProductDetail="Product 24 Detail",ProductPrice="645",ProductImage="../Images/2.jpg"},
        };
    }
    
}
