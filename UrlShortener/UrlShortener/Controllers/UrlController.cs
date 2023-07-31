using Domain.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UrlModel model)
        {
            try
            {
                var url = _urlService.CreateUrl(model.OriginalUrl);
                return Ok(url);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("shortCode")]
        public IActionResult RedirectToUrl(string shortCode)
        {
            try
            {
                var shortUrl = _urlService.GetUrlByShortCode(shortCode);
                return Ok(shortUrl);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("urls")]
        public async Task<IActionResult> GetUrls()
        {
            var userUrls = await _urlService.GetUrls();

            return Ok(userUrls);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUrl(int id)
        {
            try
            {
                _urlService.DeleteUrl(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}