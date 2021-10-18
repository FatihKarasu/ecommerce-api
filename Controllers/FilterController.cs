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
    public class FilterController : ControllerBase
    {
    
        private readonly ILogger<FilterController> _logger;

        public FilterController(ILogger<FilterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Filter Get()
        {
            Filter filter=new Filter();

            filter.Colors=new List<Color>();
            filter.Sizes=new List<Size>();
            filter.Colors=Colors;
            filter.Sizes=Sizes;
            return filter;
        }
        
       
        
        public static List<Size> Sizes = new List<Size>(){
            new Size(){SizeId="1",SizeName="xs"},
            new Size(){SizeId="2",SizeName="s"},
            new Size(){SizeId="3",SizeName="m"},
            new Size(){SizeId="4",SizeName="l"},
            new Size(){SizeId="5",SizeName="xl"},
            new Size(){SizeId="6",SizeName="xxl"},

        };
         public static List<Color> Colors = new List<Color>(){
            new Color(){ColorId="1",ColorValue="#ff0000",ColorName="Red"},
            new Color(){ColorId="2",ColorValue="#00ff00",ColorName="Green"},
            new Color(){ColorId="3",ColorValue="#0000ff",ColorName="Blue"},
            new Color(){ColorId="4",ColorValue="#ffff00",ColorName="Yellow"},
            new Color(){ColorId="5",ColorValue="#ff00ff",ColorName="Magenta"},
            new Color(){ColorId="6",ColorValue="#00ffff",ColorName="Cyan"},

        };
    }
}
