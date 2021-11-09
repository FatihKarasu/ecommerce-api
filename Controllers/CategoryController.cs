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
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<CategoryComplete> Get()
        {
            List<CategoryComplete> categoryComplete=new List<CategoryComplete>();
            foreach (Category category in Categories)
            {
                CategoryComplete complete=new CategoryComplete();
                complete.SubCategories=new List<SubCategory>();
                complete.Category=category;
                foreach (SubCategory subCategory in SubCategories)
                {
                    if(category.CategoryId==subCategory.CategoryId)
                    {
                        complete.SubCategories.Add(subCategory);
                    }
                }
                categoryComplete.Add(complete);
            }
            return categoryComplete;
        }
         [HttpGet("forFilter")]
        public List<Category> GetforFilter()
        {
          
            return Categories;
        }
       

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
    }
}
