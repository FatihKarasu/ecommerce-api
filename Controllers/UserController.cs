using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace ecommerceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserController(IConfiguration configuration,IWebHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
             this._hostEnvironment = hostEnvironment;
        }
        
        [HttpGet]
        public List<User> Get(){
            return UsersList;
        }
        [Authorize]
        [HttpGet("{id}")]
        public UserEdit Get(string id){
            UserEdit user = new UserEdit();
            foreach (User u in UsersList)
            {
                if(u.UserId==id)
                {
                    user.UserName=u.UserName;
                    user.Email=u.Email;
                }
                
            }
            return user;
        }
        [HttpPost("login")]
        public Response Login([FromForm] Login login)
        {
            Response r = new Response();
            r.Message="Login attemp failed. Please try again.";
            r.Status="danger";
            var user = AuthenticateUser(login);
   
            if (user != null)
            {
                r.UserId=user.UserId;
                r.Token= GenerateToken(user.UserId);
                r.Message="Logging In";
                r.Status="success";
            }

            return r;
        }

        [HttpPost("register")]
        public Response Register([FromForm] User user)
        {
            Response r = new Response();
            
            if(isRegistered(user.Email))
            {
                r.Message=user.Email+" is already registered.";
                r.Status="danger";
                return r;
            }
            try
            {
                user.UserId = (UsersList.Count() + 1).ToString();
                UsersList.Add(user);
                r.UserId=user.UserId;
                r.Token= GenerateToken(user.UserId);
                r.Message="Welcome "+user.UserName+".";
                r.Status="success";
            }
            catch (System.Exception)
            {
                r.Message="Sign up attemp failed. Please try again.";
                r.Status="danger";
                return r;
            }
            return r;
        }
        [Authorize]
        [HttpPost("edit")]
        public Response Edit([FromForm] User user)
        {
            Response r = new Response();
            r.Message="Save attemp failed. Please try again.";
            r.Status="danger";
           
   
            foreach (User u in UsersList)
            {
                if(u.UserId==user.UserId)
                {
                     if(isRegistered(user.Email)&&user.Email!=u.Email)
                    {
                        r.Message=user.Email+" is already registered.";
                        r.Status="danger";
                        return r;
                    }
                    if (user.UserName != null && user.UserName!=u.UserName)
                    {
                        u.UserName = user.UserName;
                    }
                    if (user.Email != null && user.Email!=u.Email)
                    {
                        u.Email = user.Email;
                    }
                    if (user.Password != null && user.Password!=u.Password)
                    {
                        u.Password = user.Password;
                    }
                    r.Message="Changes Saved.";
                    r.Status="success";
                }
                
            }

            return r;
        }
        public string GenerateToken(string userId)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private User AuthenticateUser(Login login)
        {
            foreach (User u in UsersList)
            {
                if (login.Email == u.Email && login.Password == u.Password)
                {
                    return u;
                }
            }

            return null;
        }
        private bool isRegistered(string email)
        {
            foreach (User u in UsersList)
            {
                if (email == u.Email)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<User> UsersList = new List<User>(){
            new User() {UserId="1",UserName="Fatih Karasu",Email="fatihkarasu_@hotmail.com",Password="123"},
            new User() {UserId="2",UserName="Fatih ",Email="xd@xd.com",Password="123"},
            new User() {UserId="3",UserName="xd ",Email="xd1@xd.com",Password="123" },
        };
    }
}
