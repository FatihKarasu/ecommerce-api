using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ecommerceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<ProductComplete> Get([FromQuery(Name = "start")] int start, [FromQuery(Name = "end")] int end, [FromQuery(Name = "orderBy")] string orderBy,
        [FromQuery(Name = "color")] List<string> colorIds,
        [FromQuery(Name = "size")] List<string> sizeIds,
        [FromQuery(Name = "min")] int min,
        [FromQuery(Name = "max")] int max)
        {
            List<Product> products = productsList;

            if (colorIds.Count()!= 0)
            {
                
                products=(from pc in ProductColors 
                join product in products 
                on pc.ProductId equals product.ProductId
                where colorIds.Contains(pc.ColorId)
                select product).Distinct().ToList();
                
            }
            if (sizeIds.Count()!= 0)
            {
                products=(from ps in ProductSizes 
                join product in products 
                on ps.ProductId equals product.ProductId
                where sizeIds.Contains(ps.SizeId)
                select product).Distinct().ToList();
            }
           if(max!=0)
           {
               products=(from product in products
               where Int32.Parse(product.ProductPrice) <= max && Int32.Parse(product.ProductPrice) >= min
               select product).Distinct().ToList();
           }

           

            return GetProductComplete(OrderBy(products,orderBy,start,end));
        }
       
        [HttpGet("{id}")]
        public Product GetById(string id)
        {
            Product product = new Product();
            foreach (Product p in productsList)
            {
                if (p.ProductId == id)
                {
                    product = p;
                }
            }
            return product;
        }
        public List<Product> OrderBy(List<Product> products,string orderBy,int start,int end)
        {
            if (orderBy == "newest")
            {
                return products.OrderByDescending(p =>int.Parse(p.ProductId)).Skip(start).Take(end-start).ToList();
                
            }
            if (orderBy == "oldest")
            {
                return products.OrderBy(p =>int.Parse(p.ProductId)).Skip(start).Take(end-start).ToList();
                
            }
            if (orderBy == "highest")
            {
                return products.OrderByDescending(p =>int.Parse(p.ProductPrice)).Skip(start).Take(end-start).ToList();
                
            }
            if (orderBy == "lowest")
            {
                return products.OrderBy(p =>int.Parse(p.ProductPrice)).Skip(start).Take(end-start).ToList();
                
            }
            return products;
        }
        public List<ProductComplete> GetProductComplete(List<Product> products)
        {
           List<ProductComplete> productCompletes=new List<ProductComplete>();

           foreach (Product p in products)
           {
               ProductComplete productComplete= new ProductComplete();
               productComplete.Colors=new List<Color>();
               productComplete.Sizes=new List<Size>();
               productComplete.Product=p;
               foreach (ProductColor pc in ProductColors)
               {
                   if(p.ProductId==pc.ProductId)
                   {
                       foreach (Color c in Colors)
                       {
                           if(pc.ColorId==c.ColorId)
                           {
                                productComplete.Colors.Add(c);
                           }
                       }
                   }
               }
               foreach (ProductSize ps in ProductSizes)
               {
                   if(p.ProductId==ps.ProductId)
                   {
                       foreach (Size s in Sizes)
                       {
                           if(ps.SizeId==s.SizeId)
                           {
                                productComplete.Sizes.Add(s);
                           }
                       }
                   }
               }
               productCompletes.Add(productComplete);
           }
           return productCompletes;
        }
        public List<Product> productsList = new List<Product>(){
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
        public static List<ProductColor> ProductColors = new List<ProductColor>(){
            new ProductColor(){ProductColorId="1",ProductId="1",ColorId="1"},
            new ProductColor(){ProductColorId="2",ProductId="2",ColorId="2"},
            new ProductColor(){ProductColorId="3",ProductId="3",ColorId="3"},
            new ProductColor(){ProductColorId="4",ProductId="4",ColorId="4"},
            new ProductColor(){ProductColorId="5",ProductId="5",ColorId="5"},
            new ProductColor(){ProductColorId="6",ProductId="6",ColorId="6"},
            new ProductColor(){ProductColorId="7",ProductId="7",ColorId="1"},
            new ProductColor(){ProductColorId="8",ProductId="8",ColorId="2"},
            new ProductColor(){ProductColorId="9",ProductId="9",ColorId="3"},
            new ProductColor(){ProductColorId="10",ProductId="10",ColorId="4"},
            new ProductColor(){ProductColorId="11",ProductId="11",ColorId="5"},
            new ProductColor(){ProductColorId="12",ProductId="12",ColorId="6"},
            new ProductColor(){ProductColorId="13",ProductId="13",ColorId="1"},
            new ProductColor(){ProductColorId="14",ProductId="14",ColorId="2"},
            new ProductColor(){ProductColorId="15",ProductId="15",ColorId="3"},
            new ProductColor(){ProductColorId="16",ProductId="16",ColorId="4"},
            new ProductColor(){ProductColorId="17",ProductId="17",ColorId="5"},
            new ProductColor(){ProductColorId="18",ProductId="18",ColorId="6"},
            new ProductColor(){ProductColorId="19",ProductId="19",ColorId="1"},
            new ProductColor(){ProductColorId="20",ProductId="20",ColorId="2"},
            new ProductColor(){ProductColorId="21",ProductId="21",ColorId="3"},
            new ProductColor(){ProductColorId="22",ProductId="22",ColorId="4"},
            new ProductColor(){ProductColorId="23",ProductId="23",ColorId="5"},
            new ProductColor(){ProductColorId="24",ProductId="1",ColorId="6"},
            new ProductColor(){ProductColorId="25",ProductId="2",ColorId="1"},
            new ProductColor(){ProductColorId="26",ProductId="3",ColorId="2"},
            new ProductColor(){ProductColorId="27",ProductId="4",ColorId="3"},
            new ProductColor(){ProductColorId="28",ProductId="5",ColorId="4"},
            new ProductColor(){ProductColorId="29",ProductId="6",ColorId="5"},
            new ProductColor(){ProductColorId="30",ProductId="7",ColorId="6"},
            new ProductColor(){ProductColorId="31",ProductId="8",ColorId="1"},
            new ProductColor(){ProductColorId="32",ProductId="9",ColorId="2"},
            new ProductColor(){ProductColorId="33",ProductId="10",ColorId="3"},
            new ProductColor(){ProductColorId="34",ProductId="11",ColorId="4"},
            new ProductColor(){ProductColorId="35",ProductId="12",ColorId="5"},
            new ProductColor(){ProductColorId="36",ProductId="13",ColorId="6"},
            new ProductColor(){ProductColorId="37",ProductId="14",ColorId="1"},
            new ProductColor(){ProductColorId="38",ProductId="15",ColorId="2"},
            new ProductColor(){ProductColorId="39",ProductId="16",ColorId="3"},
            new ProductColor(){ProductColorId="40",ProductId="17",ColorId="4"},
            new ProductColor(){ProductColorId="41",ProductId="1",ColorId="3"},
            new ProductColor(){ProductColorId="42",ProductId="1",ColorId="4"},
            new ProductColor(){ProductColorId="43",ProductId="1",ColorId="5"},
            new ProductColor(){ProductColorId="44",ProductId="1",ColorId="2"},
            
        };
        public static List<Color> Colors = new List<Color>(){
            new Color(){ColorId="1",ColorValue="#ff0000",ColorName="Red"},
            new Color(){ColorId="2",ColorValue="#00ff00",ColorName="Green"},
            new Color(){ColorId="3",ColorValue="#0000ff",ColorName="Blue"},
            new Color(){ColorId="4",ColorValue="#ffff00",ColorName="Yellow"},
            new Color(){ColorId="5",ColorValue="#ff00ff",ColorName="Magenta"},
            new Color(){ColorId="6",ColorValue="#00ffff",ColorName="Cyan"},

        };
        public static List<ProductSize> ProductSizes = new List<ProductSize>(){
            new ProductSize(){ProductSizeId="1",ProductId="1",SizeId="1"},
            new ProductSize(){ProductSizeId="2",ProductId="2",SizeId="2"},
            new ProductSize(){ProductSizeId="3",ProductId="3",SizeId="3"},
            new ProductSize(){ProductSizeId="4",ProductId="4",SizeId="4"},
            new ProductSize(){ProductSizeId="5",ProductId="5",SizeId="5"},
            new ProductSize(){ProductSizeId="6",ProductId="6",SizeId="6"},
            new ProductSize(){ProductSizeId="7",ProductId="7",SizeId="1"},
            new ProductSize(){ProductSizeId="8",ProductId="8",SizeId="2"},
            new ProductSize(){ProductSizeId="9",ProductId="9",SizeId="3"},
            new ProductSize(){ProductSizeId="10",ProductId="10",SizeId="4"},
            new ProductSize(){ProductSizeId="11",ProductId="11",SizeId="5"},
            new ProductSize(){ProductSizeId="12",ProductId="12",SizeId="6"},
            new ProductSize(){ProductSizeId="13",ProductId="13",SizeId="1"},
            new ProductSize(){ProductSizeId="14",ProductId="14",SizeId="2"},
            new ProductSize(){ProductSizeId="15",ProductId="15",SizeId="3"},
            new ProductSize(){ProductSizeId="16",ProductId="16",SizeId="4"},
            new ProductSize(){ProductSizeId="17",ProductId="17",SizeId="5"},
            new ProductSize(){ProductSizeId="18",ProductId="18",SizeId="6"},
            new ProductSize(){ProductSizeId="19",ProductId="19",SizeId="1"},
            new ProductSize(){ProductSizeId="20",ProductId="20",SizeId="2"},
            new ProductSize(){ProductSizeId="21",ProductId="21",SizeId="3"},
            new ProductSize(){ProductSizeId="22",ProductId="22",SizeId="4"},
            new ProductSize(){ProductSizeId="23",ProductId="23",SizeId="5"},
            new ProductSize(){ProductSizeId="24",ProductId="1",SizeId="6"},
            new ProductSize(){ProductSizeId="25",ProductId="2",SizeId="1"},
            new ProductSize(){ProductSizeId="26",ProductId="3",SizeId="2"},
            new ProductSize(){ProductSizeId="27",ProductId="4",SizeId="3"},
            new ProductSize(){ProductSizeId="28",ProductId="5",SizeId="4"},
            new ProductSize(){ProductSizeId="29",ProductId="6",SizeId="5"},
            new ProductSize(){ProductSizeId="30",ProductId="7",SizeId="6"},
            new ProductSize(){ProductSizeId="31",ProductId="8",SizeId="1"},
            new ProductSize(){ProductSizeId="32",ProductId="9",SizeId="2"},
            new ProductSize(){ProductSizeId="33",ProductId="10",SizeId="3"},
            new ProductSize(){ProductSizeId="34",ProductId="11",SizeId="4"},
            new ProductSize(){ProductSizeId="35",ProductId="12",SizeId="5"},
            new ProductSize(){ProductSizeId="36",ProductId="13",SizeId="6"},
            new ProductSize(){ProductSizeId="37",ProductId="14",SizeId="1"},
            new ProductSize(){ProductSizeId="38",ProductId="15",SizeId="2"},
            new ProductSize(){ProductSizeId="39",ProductId="16",SizeId="3"},
            new ProductSize(){ProductSizeId="40",ProductId="17",SizeId="4"},
            new ProductSize(){ProductSizeId="41",ProductId="1",SizeId="3"},
            new ProductSize(){ProductSizeId="42",ProductId="1",SizeId="4"},
            new ProductSize(){ProductSizeId="43",ProductId="1",SizeId="5"},
            new ProductSize(){ProductSizeId="44",ProductId="1",SizeId="6"},
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
