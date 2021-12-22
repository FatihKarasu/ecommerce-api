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

        [HttpGet("{userId}/{productId}")]
        public Review Get(string userId, string productId)
        {
            Review _review = new Review();

            foreach (Review review in reviewList)
            {
                if (review.UserId == userId && review.ProductId == productId && review.IsDeleted == false)
                {
                    _review = review;
                    break;
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
                if (review.ProductId == productId && review.IsDeleted == false)
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

        [Authorize]
        [HttpPost]
        public Review AddReview([FromForm] Review review)
        {
            review.ReviewId = (reviewList.Count + 1).ToString();
            review.Date = DateTime.Now.ToString();
            reviewList.Add(review);
            return review;
        }

        [Authorize]
        [HttpPost("update")]
        public Review UpdateReview([FromForm] Review review)
        {
            foreach (Review r in reviewList)
            {
                if (r.ReviewId == review.ReviewId)
                {
                    r.ReviewText = review.ReviewText;
                    r.Rating = review.Rating;
                    r.Date = DateTime.Now.ToString();
                    break;
                }
            }

            return review;
        }
        [Authorize]
        [HttpPost("delete/{reviewId}")]
        public int DeleteReview(string reviewId)
        {
            foreach (Review r in reviewList)
            {
                if (r.ReviewId == reviewId)
                {
                    r.IsDeleted = true;
                    break;
                }
            }

            return 0;
        }
        public static List<Review> reviewList = new List<Review>() { };
        public static int GenerateReviews()
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            Random random = new Random();
            for (int i = 1; i < 1400; i++)
            {
                Review review = new Review()
                {
                    ReviewId = i.ToString(),
                    ProductId = random.Next(1, 200).ToString(),
                    UserId = random.Next(1, 1000).ToString(),
                    ReviewText = ("Review " + i.ToString() + " Text"),
                    Rating = random.Next(1, 6).ToString(),
                    Date = start.AddDays(random.Next(range)).ToString(),
                    IsDeleted = false,
                };
                reviewList.Add(review);
            }
            return 0;
        }
    }
}
