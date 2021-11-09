using System;
using System.Collections.Generic;

namespace ecommerceApi
{
    public class CategoryComplete
    {
        public Category Category { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
