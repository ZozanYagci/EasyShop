using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheTestController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CacheTestController> logger;

        public CacheTestController(ICacheService cacheService, ILogger<CacheTestController> logger)
        {
            _cacheService = cacheService;
            this.logger = logger;
        }

        public record CacheSetRequest(string Key, string Value, int? ExpirationMinutes);

        [HttpPost("set")]
        public async Task<IActionResult> Set([FromBody] CacheSetRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Key))
                return BadRequest("Key and body required.");
            var expiration = request.ExpirationMinutes.HasValue ? TimeSpan.FromMinutes(request.ExpirationMinutes.Value)
                     : (TimeSpan?)null;

            await _cacheService.SetAsync<string>(request.Key, request.Value, expiration);
            logger.LogInformation("Set request received for key: {Key}", request.Key);
            return Ok(new { success = true, key = request.Key });
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] string key)
        {
            if (string.IsNullOrWhiteSpace(key))

                return BadRequest("Key is required");

            var value = await _cacheService.GetAsync<string>(key);
            if (value == null)
                return NotFound(new { found = false, key });

            return Ok(new { found = true, value });
        }
        [HttpGet("exists")]
        public async Task<IActionResult> Exists([FromQuery] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest("Key is required.");

            var exists = await _cacheService.ExistsAsync(key);
            return Ok(new { key, exists });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest("Key is required.");

            var removed = await _cacheService.RemoveAsync(key);
            return Ok(new { key, removed });
        }
    }
}
