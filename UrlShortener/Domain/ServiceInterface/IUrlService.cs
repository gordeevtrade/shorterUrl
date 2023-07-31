using UrlShortener.Dal.Entity;

namespace Domain.ServiceInterface
{
    public interface IUrlService
    {
        public ShortUrl CreateUrl(string originalUrl);

        public void DeleteUrl(int id);

        public Task<List<ShortUrl>> GetUrls();

        public ShortUrl GetUrlByShortCode(string shortCode);
    }
}