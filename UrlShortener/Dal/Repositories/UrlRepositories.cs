using Microsoft.EntityFrameworkCore;
using UrlShortener.Dal;
using UrlShortener.Dal.Entity;

namespace Dal.Repositories
{
    public class UrlRepositories : IShortUrlRepository
    {
        protected readonly ShortenerContext _context;

        public UrlRepositories(ShortenerContext context)
        {
            _context = context;
        }

        public ShortUrl Create(ShortUrl url)
        {
            _context.ShortUrls.Add(url);
            _context.SaveChanges();
            return url;
        }

        public void Delete(ShortUrl url)
        {
            _context.ShortUrls.Remove(url);
            _context.SaveChanges();
        }

        public ShortUrl GetByIdAndUserId(int id, string userId)
        {
            return _context.ShortUrls.FirstOrDefault(url => url.UserId == userId && url.UrlId == id);
        }

        public async Task<List<ShortUrl>> GetAllByUserId(string userId)
        {
            return await _context.ShortUrls.Where(url => url.UserId == userId).ToListAsync();
        }

        public ShortUrl GetByShortCode(string shortCode)
        {
            return _context.ShortUrls.FirstOrDefault(s => s.ShortenedUrl == shortCode);
        }
    }
}