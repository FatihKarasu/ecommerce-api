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
    public class SubCategoryController : ControllerBase
    {

        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(ILogger<SubCategoryController> logger)
        {
            _logger = logger;
        }

       
        [HttpGet]
        public List<SubCategory> Get()
        {
         
            return SubCategories;
        }

        public static List<SubCategory> SubCategories = new List<SubCategory>(){
            new SubCategory(){CategoryId="women",SubCategoryName="Women Sub",SubCategoryId="women-sub"},
            new SubCategory(){CategoryId="men",SubCategoryName="Men Sub",SubCategoryId="men-sub"},
            new SubCategory(){CategoryId="kids",SubCategoryName="Kids Sub",SubCategoryId="kids-sub"},
            new SubCategory(){CategoryId="baby",SubCategoryName="Baby Sub",SubCategoryId="baby-sub"},

        };
    }
}
