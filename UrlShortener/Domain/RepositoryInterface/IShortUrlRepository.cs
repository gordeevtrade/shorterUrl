using UrlShortener.Dal.Entity;

namespace Dal.Repositories
{
    public interface IShortUrlRepository
    {
        ShortUrl GetByIdAndUserId(int id, string userId);

        ShortUrl Create(ShortUrl url);

        void Delete(ShortUrl url);

        Task<List<ShortUrl>> GetAllByUserId(string userId);

        ShortUrl GetByShortCode(string shortCode);
    }
}