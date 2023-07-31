using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dal.Entity
{
    public class ShortUrl
    {
        [Key]
        public int UrlId { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}