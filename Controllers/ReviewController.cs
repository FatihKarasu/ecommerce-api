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
    public class ReviewController : ControllerBase
    {

        private readonly ILogger<ReviewController> _logger;

        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
            if (reviewList.Count() == 0)
            {
                GenerateReviews();
            }
        }
   
        [HttpGet("{reviewId}")]
        public Review Get(string reviewId)
        {
            Review _review = new Review();

            foreach (Review review in reviewList)
            {
                if (review.ReviewId == reviewId)
                {
                    _review = review;
                }

            }

            return _review;
        }
        [HttpGet]
        public List<Review> Get([FromQuery(Name = "productId")] string productId, 
        [FromQuery(Name = "start")] int start,
        [FromQuery(Name = "end")] int end, 
        [FromQuery(Name = "orderBy")] string orderBy)
        {

            List<Review> reviews = new List<Review>();
            foreach (Review review in reviewList)
            {
                if (review.ProductId == productId)
                {
                    reviews.Add(review);
                }
            }
            if (orderBy == "oldest")
            {
                return reviews.OrderBy(r => int.Parse(r.ReviewId)).Skip(start).Take(end - start).ToList();

            }
            if (orderBy == "highest")
            {
                return reviews.OrderByDescending(r => int.Parse(r.Rating)).Skip(start).Take(end - start).ToList();

            }
            if (orderBy == "lowest")
            {
                return reviews.OrderBy(r => int.Parse(r.Rating)).Skip(start).Take(end - start).ToList();

            }


            return reviews.OrderByDescending(r => int.Parse(r.ReviewId)).Skip(start).Take(end - start).ToList();
        }


        public static List<Review> reviewList = new List<Review>() { };
        public static int GenerateReviews()
        {
            Random random = new Random();
            for (int i = 1; i < 1400; i++)
            {
                Review review = new Review()
                {
                    ReviewId = i.ToString(),
                    ProductId = random.Next(1, 200).ToString(),
                    UserId = random.Next(1, 3).ToString(),
                    ReviewText = ("Review " + i.ToString() + " Text"),
                    Rating = random.Next(1, 6).ToString()
                };
                reviewList.Add(review);
            }
            return 0;
        }
    }
}
