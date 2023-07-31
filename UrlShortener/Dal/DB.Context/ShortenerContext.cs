using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Dal.Entity;

namespace UrlShortener.Dal
{
    public class ShortenerContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public ShortenerContext(DbContextOptions<ShortenerContext> options)
         : base(options)
        {
        }

        public ShortenerContext()
        {
        }
    }
}