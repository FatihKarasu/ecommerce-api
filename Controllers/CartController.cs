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
            if (ProductsController.productsList.Count() == 0)
            {
                ProductsController.GenerateProducts();
                ProductsController.GenerateProductColors();
                ProductsController.GenerateProductSizes();
            }
        }
        [HttpGet]
        public List<CartItem> Get()
        {
            return cartItemList;
        }
        [HttpGet("user/{userId}")]
        public List<CartComplete> GetById(string userId)
        {
            List<CartComplete> items = new List<CartComplete>();
            
            foreach (CartItem item in cartItemList)
            {
                if (item.UserId == userId)
                {
                    foreach (Product product in ProductsController.productsList)
                    {
                        if (item.ProductId == product.ProductId)
                        {
                            CartComplete complete = new CartComplete();
                            complete.Product = new Product();
                            complete.Color = new Color();
                            complete.Size = new Size();

                            complete.CartItemId = item.CartItemId;
                            complete.UserId = item.UserId;
                            complete.Product = product;
                            foreach (Color color in ProductsController.Colors)
                            {
                                if (color.ColorId == item.ColorId)
                                {
                                    complete.Color = color;
                                    break;
                                }
                            }
                            foreach (Size size in ProductsController.Sizes)
                            {
                                if (size.SizeId == item.SizeId)
                                {
                                    complete.Size = size;
                                    break;
                                }
                            }
                            complete.Amount = item.Amount;
                            items.Add(complete);

                            break;
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
            CartComplete complete = new CartComplete();
            foreach (CartItem cartItem in cartItemList)
            {
                if (cartItem.UserId == item.UserId && cartItem.ProductId == item.ProductId && cartItem.ColorId==item.ColorId && cartItem.SizeId== item.SizeId )
                {
                    item.CartItemId = cartItem.CartItemId;
                    item.Amount += cartItem.Amount;
                    cartItem.Amount = item.Amount;
                }
            }
            if (item.CartItemId == null)
            {
                if (cartItemList.Count != 0)
                {
                    item.CartItemId = (Int32.Parse(cartItemList.LastOrDefault().CartItemId) + 1).ToString();

                }
                else
                {
                    item.CartItemId = "1";
                }
                cartItemList.Add(item);
            }
            foreach (Product product in ProductsController.productsList)
            {
                if (item.ProductId == product.ProductId)
                {
                    
                    complete.Product = new Product();
                    complete.Color = new Color();
                    complete.Size = new Size();

                    complete.CartItemId = item.CartItemId;
                    complete.UserId = item.UserId;
                    complete.Product = product;
                    foreach (Color color in ProductsController.Colors)
                    {
                        if (color.ColorId == item.ColorId)
                        {
                            complete.Color = color;
                        }
                    }
                    foreach (Size size in ProductsController.Sizes)
                    {
                        if (size.SizeId == item.SizeId)
                        {
                            complete.Size = size;
                        }
                    }
                    complete.Amount = item.Amount;

                }
            }

            return complete;
        }
        [Authorize]
        [HttpDelete("delete/{cartItemId}")]
        public Response DeleteCartItem(string cartItemId)
        {
            Response response = new Response();
            response.Message = "Please try again.";
            response.Status = "danger";
            CartItem item = cartItemList.SingleOrDefault(item => item.CartItemId == cartItemId);
            if (item != null)
            {
                cartItemList.Remove(item);
                response.Message = "Item Deleted.";
                response.Status = "success";
            }

            return response;
        }

        [Authorize]
        [HttpPost("changeamount")]
        public Response ChangeAmount([FromForm] CartItem item)
        {
            Response response = new Response();
            response.Message = "Please try again.";
            response.Status = "danger";
            foreach (CartItem cartItem in cartItemList)
            {
                if (cartItem.CartItemId == item.CartItemId)
                {
                    cartItem.Amount = item.Amount;
                    response.Message = "";
                    response.Status = "success";
                }
            }
            return response;
        }
        public static List<CartItem> cartItemList = new List<CartItem>(){
            new CartItem(){CartItemId="1",UserId="1",ProductId="1",ColorId="1",SizeId="1",Amount=2},
            new CartItem(){CartItemId="2",UserId="1",ProductId="2",ColorId="2",SizeId="2",Amount=1},
            new CartItem(){CartItemId="3",UserId="1",ProductId="3",ColorId="3",SizeId="3",Amount=3},
            new CartItem(){CartItemId="4",UserId="1",ProductId="4",ColorId="4",SizeId="4",Amount=5},
            new CartItem(){CartItemId="5",UserId="1",ProductId="5",ColorId="5",SizeId="6",Amount=4},
        };

      
    }

}
