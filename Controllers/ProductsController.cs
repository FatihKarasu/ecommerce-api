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
            if (productsList.Count() == 0)
            {
                GenerateProducts();
                GenerateProductColors();
                GenerateProductSizes();
            }
            if (ReviewController.reviewList.Count() == 0)
            {
                ReviewController.GenerateReviews();
            }
            if (CampaignController.campaignProducts.Count() == 0)
            {
                CampaignController.GenerateCampaignProducts();
            }


        }
        [HttpGet]
        public List<ProductComplete> Get([FromQuery(Name = "campaignId")] string campaignId, [FromQuery(Name = "query")] string query, [FromQuery(Name = "categoryId")] string categoryId, [FromQuery(Name = "start")] int start, [FromQuery(Name = "end")] int end, [FromQuery(Name = "orderBy")] string orderBy,
        [FromQuery(Name = "color")] List<string> colorIds,
        [FromQuery(Name = "size")] List<string> sizeIds,
        [FromQuery(Name = "min")] int min,
        [FromQuery(Name = "max")] int max)
        {
            List<Product> products = productsList;
            List<string> subCategories = new List<string>();
            if (query != null && query.Length >= 3)
            {
                products = (from p in products
                            where p.ProductTitle.ToLower().Contains(query.ToLower()) || p.ProductDetail.ToLower().Contains(query.ToLower())
                            select p).Distinct().ToList();
            }
            else if (campaignId != null)
            {
                products = (from cp in CampaignController.campaignProducts join p in products on cp.ProductId equals p.ProductId where cp.CampaignId == campaignId select p).Distinct().ToList();
            }
            if (categoryId != null)
            {
                for (int i = 0; i < Categories.Count(); i++)
                {
                    if (Categories[i].CategoryId == categoryId)
                    {
                        for (int x = 0; x < SubCategories.Count(); x++)
                        {
                            if (SubCategories[x].CategoryId == Categories[i].CategoryId)
                            {
                                subCategories.Add(SubCategories[x].SubCategoryId);
                            }
                        }
                        break;
                    }
                }
                if (subCategories.Count() != 0)
                {
                    products = (from p in products
                                where subCategories.Contains(p.SubCategoryId)
                                select p).Distinct().ToList();

                }
                else
                {
                    products = (from p in products
                                where p.SubCategoryId == categoryId
                                select p).Distinct().ToList();
                }
            }

            if (colorIds.Count() != 0)
            {
                products = (from pc in ProductColors
                            join product in products
                            on pc.ProductId equals product.ProductId
                            where colorIds.Contains(pc.ColorId)
                            select product).Distinct().ToList();

            }
            if (sizeIds.Count() != 0)
            {
                products = (from ps in ProductSizes
                            join product in products
                            on ps.ProductId equals product.ProductId
                            where sizeIds.Contains(ps.SizeId)
                            select product).Distinct().ToList();
            }
            if (max != 0)
            {
                products = (from product in products
                            where Int32.Parse(product.ProductPrice) <= max && Int32.Parse(product.ProductPrice) >= min
                            select product).Distinct().ToList();
            }



            return GetProductComplete(OrderBy(products, orderBy, start, end));
        }

        [HttpGet("{id}")]
        public ProductComplete GetById(string id)
        {
            ProductComplete product = new ProductComplete();
            product.Colors = new List<Color>();
            product.Sizes = new List<Size>();
            foreach (Product p in productsList)
            {
                if (p.ProductId == id)
                {
                    var reviewCount = 0;
                    var rating = 0;
                    product.Product = p;
                    foreach (ProductColor pc in ProductColors)
                    {
                        if (p.ProductId == pc.ProductId)
                        {
                            foreach (Color c in Colors)
                            {
                                if (pc.ColorId == c.ColorId)
                                {
                                    product.Colors.Add(c);
                                }
                            }
                        }
                    }
                    foreach (ProductSize ps in ProductSizes)
                    {
                        if (p.ProductId == ps.ProductId)
                        {
                            foreach (Size s in Sizes)
                            {
                                if (ps.SizeId == s.SizeId)
                                {
                                    product.Sizes.Add(s);
                                }
                            }
                        }
                    }
                    foreach (Review review in ReviewController.reviewList)
                    {
                        if (p.ProductId == review.ProductId && review.IsDeleted==false)
                        {
                            rating += Int32.Parse(review.Rating);
                            reviewCount += 1;
                        }
                    }
                    if (reviewCount > 0)
                    {
                        product.ReviewCount = reviewCount;
                        product.Rating = Math.Round(((double)rating / (double)reviewCount), 1).ToString();
                    }

                }

            }
            product.Colors = product.Colors.Distinct().OrderBy(c => int.Parse(c.ColorId)).ToList();
            product.Sizes = product.Sizes.Distinct().OrderBy(s => int.Parse(s.SizeId)).ToList();
            return product;
        }
        public List<Product> OrderBy(List<Product> products, string orderBy, int start, int end)
        {
            if (orderBy == "newest")
            {
                return products.OrderByDescending(p => int.Parse(p.ProductId)).Skip(start).Take(end - start).ToList();

            }
            if (orderBy == "oldest")
            {
                return products.OrderBy(p => int.Parse(p.ProductId)).Skip(start).Take(end - start).ToList();

            }
            if (orderBy == "highest")
            {
                return products.OrderByDescending(p => int.Parse(p.ProductPrice)).Skip(start).Take(end - start).ToList();

            }
            if (orderBy == "lowest")
            {
                return products.OrderBy(p => int.Parse(p.ProductPrice)).Skip(start).Take(end - start).ToList();

            }
            return products;
        }
        public List<ProductComplete> GetProductComplete(List<Product> products)
        {
            List<ProductComplete> productCompletes = new List<ProductComplete>();

            foreach (Product p in products)
            {
                ProductComplete productComplete = new ProductComplete();
                productComplete.Colors = new List<Color>();
                productComplete.Sizes = new List<Size>();
                productComplete.Product = p;
                foreach (ProductColor pc in ProductColors)
                {
                    if (p.ProductId == pc.ProductId)
                    {
                        foreach (Color c in Colors)
                        {
                            if (pc.ColorId == c.ColorId)
                            {
                                productComplete.Colors.Add(c);
                            }
                        }
                    }
                }
                foreach (ProductSize ps in ProductSizes)
                {
                    if (p.ProductId == ps.ProductId)
                    {
                        foreach (Size s in Sizes)
                        {
                            if (ps.SizeId == s.SizeId)
                            {
                                productComplete.Sizes.Add(s);
                            }
                        }
                    }
                }

                productComplete.Colors = productComplete.Colors.Distinct().OrderBy(c => int.Parse(c.ColorId)).ToList();
                productComplete.Sizes = productComplete.Sizes.Distinct().OrderBy(s => int.Parse(s.SizeId)).ToList();
                productCompletes.Add(productComplete);
            }
            return productCompletes;
        }


        public static List<Product> productsList = new List<Product>();

        public static List<ProductColor> ProductColors = new List<ProductColor>();
        public static List<ProductSize> ProductSizes = new List<ProductSize>();

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
        public static List<Category> Categories = new List<Category>(){
            new Category(){CategoryId="women",CategoryName="Women",CategoryImage1="../Images/1.jpg",CategoryImage2="../Images/2.jpg"},
            new Category(){CategoryId="men",CategoryName="Men",CategoryImage1="../Images/3.jpg",CategoryImage2="../Images/1.jpg"},
            new Category(){CategoryId="kids",CategoryName="Kids",CategoryImage1="../Images/2.jpg",CategoryImage2="../Images/3.jpg"},
            new Category(){CategoryId="baby",CategoryName="Baby",CategoryImage1="../Images/3.jpg",CategoryImage2="../Images/2.jpg"},
        };
        public static List<SubCategory> SubCategories = new List<SubCategory>(){
            new SubCategory(){CategoryId="women",SubCategoryName="Women Sub",SubCategoryId="women-sub",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/2.jpg"},
            new SubCategory(){CategoryId="women",SubCategoryName="Women Sub2",SubCategoryId="women-sub2",SubCategoryImage1="../Images/2.jpg",SubCategoryImage2="../Images/3.jpg"},
            new SubCategory(){CategoryId="women",SubCategoryName="Women Sub3",SubCategoryId="women-sub3",SubCategoryImage1="../Images/3.jpg",SubCategoryImage2="../Images/1.jpg"},
            new SubCategory(){CategoryId="women",SubCategoryName="Women Sub4",SubCategoryId="women-sub4",SubCategoryImage1="../Images/3.jpg",SubCategoryImage2="../Images/2.jpg"},
            new SubCategory(){CategoryId="men",SubCategoryName="Men Sub",SubCategoryId="men-sub",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/2.jpg"},
            new SubCategory(){CategoryId="men",SubCategoryName="Men Sub2",SubCategoryId="men-sub2",SubCategoryImage1="../Images/3.jpg",SubCategoryImage2="../Images/1.jpg"},
            new SubCategory(){CategoryId="men",SubCategoryName="Men Sub3",SubCategoryId="men-sub3",SubCategoryImage1="../Images/2.jpg",SubCategoryImage2="../Images/3.jpg"},
            new SubCategory(){CategoryId="men",SubCategoryName="Men Sub4",SubCategoryId="men-sub4",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/3.jpg"},
            new SubCategory(){CategoryId="kids",SubCategoryName="Kids Sub",SubCategoryId="kids-sub",SubCategoryImage1="../Images/2.jpg",SubCategoryImage2="../Images/1.jpg"},
            new SubCategory(){CategoryId="kids",SubCategoryName="Kids Sub2",SubCategoryId="kids-sub2",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/2.jpg"},
            new SubCategory(){CategoryId="kids",SubCategoryName="Kids Sub3",SubCategoryId="kids-sub3",SubCategoryImage1="../Images/3.jpg",SubCategoryImage2="../Images/1.jpg"},
            new SubCategory(){CategoryId="kids",SubCategoryName="Kids Sub4",SubCategoryId="kids-sub4",SubCategoryImage1="../Images/2.jpg",SubCategoryImage2="../Images/3.jpg"},
            new SubCategory(){CategoryId="baby",SubCategoryName="Baby Sub",SubCategoryId="baby-sub",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/2.jpg"},
            new SubCategory(){CategoryId="baby",SubCategoryName="Baby Sub2",SubCategoryId="baby-sub2",SubCategoryImage1="../Images/3.jpg",SubCategoryImage2="../Images/1.jpg"},
            new SubCategory(){CategoryId="baby",SubCategoryName="Baby Sub3",SubCategoryId="baby-sub3",SubCategoryImage1="../Images/2.jpg",SubCategoryImage2="../Images/3.jpg"},
            new SubCategory(){CategoryId="baby",SubCategoryName="Baby Sub4",SubCategoryId="baby-sub4",SubCategoryImage1="../Images/1.jpg",SubCategoryImage2="../Images/2.jpg"},

        };

        public static int GenerateProducts()
        {
            Random random = new Random();
            for (int i = 1; i < 200; i++)
            {
                string oldPrice = random.Next(20, 600).ToString();
                string price = null;
                if (random.Next(1, 7) == 6)
                {
                    price = random.Next(20, int.Parse(oldPrice)).ToString();
                }
                if (price == null)
                {
                    price = oldPrice;
                    oldPrice = null;
                }
                Product product = new Product()
                {
                    ProductId = i.ToString(),
                    SubCategoryId = subCategories[random.Next(0, 16)],
                    ProductTitle = ("Product " + i.ToString() + " Title"),
                    ProductDetail = "Product " + i.ToString() + " Detail",
                    ProductPrice = price,
                    ProductOldPrice = oldPrice,
                    ProductImage = images[random.Next(0, 3)]
                };
                productsList.Add(product);
            }
            return 0;
        }
        public static int GenerateProductColors()
        {
            Random random = new Random();
            for (int i = 1; i < 600; i++)
            {
                ProductColor productColor = new ProductColor() { ProductColorId = i.ToString(), ProductId = random.Next(1, 200).ToString(), ColorId = random.Next(1, 7).ToString() };
                ProductColors.Add(productColor);
            }
            return 0;
        }
        public static int GenerateProductSizes()
        {
            Random random = new Random();
            for (int i = 1; i < 600; i++)
            {
                ProductSize productSize = new ProductSize() { ProductSizeId = i.ToString(), ProductId = random.Next(1, 200).ToString(), SizeId = random.Next(1, 7).ToString() };
                ProductSizes.Add(productSize);
            }
            return 0;
        }
        public static string[] subCategories = new string[] {
         "women-sub", "women-sub2", "women-sub3", "women-sub4"
        , "men-sub", "men-sub2", "men-sub3", "men-sub4"
        , "kids-sub", "kids-sub2", "kids-sub3", "kids-sub4"
        , "baby-sub", "baby-sub2", "baby-sub3", "baby-sub4" };

        public static string[] images = new string[] { "../Images/1.jpg", "../Images/2.jpg", "../Images/3.jpg" };

    }
}
