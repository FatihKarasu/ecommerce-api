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
    public class CampaignController : ControllerBase
    {
    
        private readonly ILogger<CampaignController> _logger;

        public CampaignController(ILogger<CampaignController> logger)
        {
            _logger = logger;
            if(campaignProducts.Count()==0)
            {
                GenerateCampaignProducts();
            }
        }

        [HttpGet]
        public List<Campaign> Get()
        {
           
            return campaigns;
        }
        [HttpGet("xd")]
        public  List<CampaignProduct> Getxd()
        {
           
            return campaignProducts;
        }
       
        
        public static List<Campaign> campaigns = new List<Campaign>(){
            new Campaign(){CampaignId="black-friday-sale",CampaignTitle="Black Friday Sale",CampaignDescription="Black Friday Sale Description",CampaignImage="../Images/1.jpg"},
            new Campaign(){CampaignId="mothers-day-sale",CampaignTitle="Mother's Day Sale",CampaignDescription="Mother's Day Sale Description",CampaignImage="../Images/2.jpg"},
            new Campaign(){CampaignId="fathers-day-sale",CampaignTitle="Father's Day Sale",CampaignDescription="Father's Day Sale Description",CampaignImage="../Images/3.jpg"},
            

        };
         public static List<CampaignProduct> campaignProducts = new List<CampaignProduct>();
         public static int GenerateCampaignProducts()
        {
            Random random = new Random();
            for (int i = 1; i < 200; i++)
            {
                CampaignProduct campaignProduct = new CampaignProduct() { CampaignProductId = i.ToString(),CampaignId=campaigns[random.Next(0, 3)].CampaignId, ProductId = random.Next(1, 200).ToString() };
                campaignProducts.Add(campaignProduct);
            }
            return 0;
        }
    }
}
