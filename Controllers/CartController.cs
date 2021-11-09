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
                            foreach (Color color in Colors)
                            {
                                if (color.ColorId == item.ColorId)
                                {
                                    complete.Color = color;
                                }
                            }
                            foreach (Size size in Sizes)
                            {
                                if (size.SizeId == item.SizeId)
                                {
                                    complete.Size = size;
                                }
                            }
                            complete.Amount = item.Amount;
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
                    foreach (Color color in Colors)
                    {
                        if (color.ColorId == item.ColorId)
                        {
                            complete.Color = color;
                        }
                    }
                    foreach (Size size in Sizes)
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

        public static List<Product> productsList = new List<Product>(){
            new Product(){ProductId="1",ProductTitle="Product 1 Title",ProductDetail="Product 1 Detail",ProductPrice="20",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="2",ProductTitle="Product 2 Title",ProductDetail="Product 2 Detail",ProductPrice="25",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="3",ProductTitle="Product 3 Title",ProductDetail="Product 3 Detail",ProductPrice="30",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="4",ProductTitle="Product 4 Title",ProductDetail="Product 4 Detail",ProductPrice="35",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="5",ProductTitle="Product 5 Title",ProductDetail="Product 5 Detail",ProductPrice="40",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="6",ProductTitle="Product 6 Title",ProductDetail="Product 6 Detail",ProductPrice="45",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="7",ProductTitle="Product 7 Title",ProductDetail="Product 7 Detail",ProductPrice="50",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="8",ProductTitle="Product 8 Title",ProductDetail="Product 8 Detail",ProductPrice="55",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="9",ProductTitle="Product 9 Title",ProductDetail="Product 9 Detail",ProductPrice="60",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="10",ProductTitle="Product 10 Title",ProductDetail="Product 10 Detail",ProductPrice="65",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="11",ProductTitle="Product 11 Title",ProductDetail="Product 11 Detail",ProductPrice="70",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="12",ProductTitle="Product 12 Title",ProductDetail="Product 12 Detail",ProductPrice="75",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="13",ProductTitle="Product 13 Title",ProductDetail="Product 13 Detail",ProductPrice="80",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="14",ProductTitle="Product 14 Title",ProductDetail="Product 14 Detail",ProductPrice="85",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="15",ProductTitle="Product 15 Title",ProductDetail="Product 15 Detail",ProductPrice="90",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="16",ProductTitle="Product 16 Title",ProductDetail="Product 16 Detail",ProductPrice="95",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="17",ProductTitle="Product 17 Title",ProductDetail="Product 17 Detail",ProductPrice="100",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="18",ProductTitle="Product 18 Title",ProductDetail="Product 18 Detail",ProductPrice="105",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="19",ProductTitle="Product 19 Title",ProductDetail="Product 19 Detail",ProductPrice="110",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="20",ProductTitle="Product 20 Title",ProductDetail="Product 20 Detail",ProductPrice="115",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="21",ProductTitle="Product 21 Title",ProductDetail="Product 21 Detail",ProductPrice="120",ProductImage="../Images/2.jpg"},
            new Product(){ProductId="22",ProductTitle="Product 22 Title",ProductDetail="Product 22 Detail",ProductPrice="125",ProductImage="../Images/3.jpg"},
            new Product(){ProductId="23",ProductTitle="Product 23 Title",ProductDetail="Product 23 Detail",ProductPrice="130",ProductImage="../Images/1.jpg"},
            new Product(){ProductId="24",ProductTitle="Product 24 Title",ProductDetail="Product 24 Detail",ProductPrice="135",ProductImage="../Images/2.jpg"},
        };
        
        public static List<Color> Colors = new List<Color>(){
            new Color(){ColorId="1",ColorValue="#ff0000",ColorName="Red"},
            new Color(){ColorId="2",ColorValue="#00ff00",ColorName="Green"},
            new Color(){ColorId="3",ColorValue="#0000ff",ColorName="Blue"},
            new Color(){ColorId="4",ColorValue="#ffff00",ColorName="Yellow"},
            new Color(){ColorId="5",ColorValue="#ff00ff",ColorName="Magenta"},
            new Color(){ColorId="6",ColorValue="#00ffff",ColorName="Cyan"},

        };

        public static List<Size> Sizes = new List<Size>(){
            new Size(){SizeId="1",SizeName="xs"},
            new Size(){SizeId="2",SizeName="s"},
            new Size(){SizeId="3",SizeName="m"},
            new Size(){SizeId="4",SizeName="l"},
            new Size(){SizeId="5",SizeName="xl"},
            new Size(){SizeId="6",SizeName="xxl"},

        };
    }

}
