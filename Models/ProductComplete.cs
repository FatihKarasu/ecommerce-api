using System;
using System.Collections.Generic;
namespace ecommerceApi
{
    public class ProductComplete
    {
        public Product Product { get; set; }

        public List<Color> Colors { get; set; }

        public List<Size> Sizes { get; set; }

        public string Rating{get;set;}

        public int ReviewCount{get;set;}
    }
}
