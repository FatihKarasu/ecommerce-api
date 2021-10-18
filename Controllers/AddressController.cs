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
    public class AddressController : ControllerBase
    {
    
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<Address> Get()
        {
            
            return addressList;
        }
        [HttpGet("{addressId}")]
        public Address GetById(string addressId)
        {
            Address _address=new Address();

            foreach (Address address in addressList)
            {
                if(address.AddressId==addressId)
                {
                    _address=address;
                }
                
            }
            
            return _address;
        }
        [HttpGet("user/{userId}")]
        public List<Address> GetByUserId(string userId)
        {
            List<Address> addresses=new List<Address>();

            foreach (Address address in addressList)
            {
                if(address.UserId==userId)
                {
                    addresses.Add(address);
                }
                
            }
            
            return addresses;
        }
        [Authorize]
        [HttpPost]
        public Address AddAddress([FromForm] Address address)
        {
            address.AddressId=(Int32.Parse(addressList.LastOrDefault().AddressId)+1).ToString();
            addressList.Add(address);
            return address;
        }

        [Authorize]
        [HttpPost("edit")]
        public Address EditAddress([FromForm] Address _address)
        {

           foreach (Address address in addressList)
            {
                if(address.AddressId==_address.AddressId)
                {
                    address.Name=_address.Name;
                    address.Title=_address.Title;
                    address.City=_address.City;
                    address.District=_address.District;
                    address.Neighbourhood=_address.Neighbourhood;
                    address.Detail=_address.Detail;
                    address.PhoneNumber=_address.PhoneNumber;
                    
                    return address;
                }
            }
            return _address;
        }
        
        [Authorize]
        [HttpDelete("delete/{addressId}")]
        public Response DeleteAddress(string addressId)
        {
            Response response = new Response();
            response.Message="Please try again.";
            response.Status="danger";
            Address address = addressList.SingleOrDefault(address => address.AddressId == addressId);
            if (address != null)
            {
                addressList.Remove(address);
                response.Message="Address Deleted.";
                response.Status="success";
            }
            
            return response;
        }
         public static  List<Address> addressList = new List<Address>(){
            new Address(){AddressId="1",UserId="1",Name="Name1",Title="Home",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},
            new Address(){AddressId="2",UserId="1",Name="Name2",Title="Work",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},
            new Address(){AddressId="3",UserId="2",Name="Name3",Title="Home",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},
            new Address(){AddressId="4",UserId="3",Name="Name4",Title="Home",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},
            new Address(){AddressId="5",UserId="3",Name="Name5",Title="Xd",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},
            new Address(){AddressId="6",UserId="2",Name="Name6",Title="XXD",City="City",District="District",Neighbourhood="Neighbourhood",Detail="AddressDetail",PhoneNumber="0123456789"},

           };
    }
}
