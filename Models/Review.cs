using System;
using System.Text.Json.Serialization;
namespace ecommerceApi
{
    public class Review
    {
        public string ReviewId { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string ReviewText { get; set; }
        public string Rating { get; set; }
        public string Date { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
