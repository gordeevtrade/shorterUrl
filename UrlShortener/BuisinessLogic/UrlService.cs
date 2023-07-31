using Dal.Repositories;
using Domain.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UrlShortener.Dal.Entity;

namespace UrlShortener.BuisinessLogic
{
    public class UrlService : IUrlService
    {
        private IShortUrlRepository _iShortUrlRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UrlService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IShortUrlRepository IShortUrlRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _iShortUrlRepository = IShortUrlRepository;
        }

        public ShortUrl CreateUrl(string originalUrl)
        {
            var userId = GetUserId();
            var user = _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var shortenedUrl = Guid.NewGuid().ToString();

            var url = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                CreatedDate = DateTime.UtcNow,
                UserId = userId
            };
            return _iShortUrlRepository.Create(url);
        }

        public async Task<List<ShortUrl>> GetUrls()
        {
            var userId = GetUserId();
            return await _iShortUrlRepository.GetAllByUserId(userId);
        }

        public void DeleteUrl(int id)
        {
            var userId = GetUserId();
            var urlToDelete = _iShortUrlRepository.GetByIdAndUserId(id, userId);
            if (urlToDelete == null)
            {
                throw new Exception(userId);
            }
            _iShortUrlRepository.Delete(urlToDelete);
        }

        public ShortUrl GetUrlByShortCode(string shortCode)
        {
            var url = _iShortUrlRepository.GetByShortCode(shortCode);
            if (url == null)
            {
                throw new Exception(shortCode);
            }
            return url;
        }

        private string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.SerialNumber)?.Value;
            return userId;
        }
    }
}