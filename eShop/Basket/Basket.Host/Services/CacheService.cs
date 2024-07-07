using Basket.Host.Configurations;
using Basket.Host.Services.Interfaces;
using StackExchange.Redis;

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly IRedisCacheConnectionService _redisCacheConnectionService;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly RedisConfig _config;

    public CacheService(
        ILogger<CacheService> logger,
        IRedisCacheConnectionService redisCacheConnectionService,
        IOptions<RedisConfig> config,
        IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _redisCacheConnectionService = redisCacheConnectionService;
        _jsonSerializer = jsonSerializer;
        _config = config.Value;
    }

    public Task AddOrUpdateAsync<T>(string key, T value)
        => AddOrUpdateInternalAsync(key, value);

    public async Task<T> GetAsync<T>(string key)
    {
        var redis = GetRedisDatabase();
        var cacheKey = GetItemCacheKey(key);
        var serialized = await redis.StringGetAsync(cacheKey);

        if (serialized.HasValue)
        {
            _logger.LogInformation($"Cache hit for key {cacheKey}");
            return _jsonSerializer.Deserialize<T>(serialized.ToString());
        }
        else
        {
            _logger.LogInformation($"Cache miss for key {cacheKey}");
            return default(T)!;
        }
    }

    private string GetItemCacheKey(string userId) =>
        $"basket:{userId}";

    private async Task AddOrUpdateInternalAsync<T>(string key, T value,
        IDatabase redis = null!, TimeSpan? expiry = null)
    {
        redis = redis ?? GetRedisDatabase();
        expiry = expiry ?? _config.CacheTimeout;

        var cacheKey = GetItemCacheKey(key);
        var serialized = _jsonSerializer.Serialize(value);

        var success = await redis.StringSetAsync(cacheKey, serialized, expiry);
        if (success)
        {
            _logger.LogInformation($"Cached value for key {cacheKey} set successfully");
        }
        else
        {
            _logger.LogWarning($"Failed to cache value for key {cacheKey}");
        }
    }

    private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
}