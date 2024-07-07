using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Infrastructure.RateLimit.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConnectionMultiplexer _redis;
        private readonly ILogger<RateLimitMiddleware> _logger;

        public RateLimitMiddleware(RequestDelegate next, ConnectionMultiplexer redis, ILogger<RateLimitMiddleware> logger)
        {
            _next = next;
            _redis = redis;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var db = _redis.GetDatabase();
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var endpoint = context.Request.Path.ToString();
            var key = $"{ipAddress}:{endpoint}";

            _logger.LogInformation($"Rate limiting key: {key}");

            var currentCount = await db.StringIncrementAsync(key);
            if (currentCount == 1)
            {
                await db.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }

            _logger.LogInformation($"Request count for {key}: {currentCount}");

            if (currentCount > 10)
            {
                _logger.LogWarning($"Rate limit exceeded for {key}");
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Too Many Requests");
                return;
            }

            await _next(context);
        }
    }
}