using Basket.Host.Configurations;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using StackExchange.Redis;

namespace Basket.UnitTests.Services
{
    public class CacheServiceTest
    {
        private readonly ICacheService _cacheService;
        private readonly Mock<IOptions<RedisConfig>> _configMock;
        private readonly Mock<ILogger<CacheService>> _loggerMock;
        private readonly Mock<IRedisCacheConnectionService> _redisCacheConnectionServiceMock;
        private readonly Mock<IJsonSerializer> _jsonSerializerMock;
        private readonly Mock<IConnectionMultiplexer> _connectionMultiplexerMock;
        private readonly Mock<IDatabase> _redisDatabaseMock;

        public CacheServiceTest()
        {
            _configMock = new Mock<IOptions<RedisConfig>>();
            _loggerMock = new Mock<ILogger<CacheService>>();
            _redisCacheConnectionServiceMock = new Mock<IRedisCacheConnectionService>();
            _jsonSerializerMock = new Mock<IJsonSerializer>();
            _connectionMultiplexerMock = new Mock<IConnectionMultiplexer>();
            _redisDatabaseMock = new Mock<IDatabase>();

            _configMock.Setup(x => x.Value).Returns(new RedisConfig { CacheTimeout = TimeSpan.Zero });

            _connectionMultiplexerMock.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_redisDatabaseMock.Object);
            _redisCacheConnectionServiceMock.Setup(x => x.Connection).Returns(_connectionMultiplexerMock.Object);

            _cacheService = new CacheService(
                _loggerMock.Object,
                _redisCacheConnectionServiceMock.Object,
                _configMock.Object,
                _jsonSerializerMock.Object);
        }

        [Fact]
        public async Task AddOrUpdateAsync_SuccessfullyCached()
        {
            // Arrange
            var key = "TestKey";
            var value = "TestValue";

            _redisDatabaseMock.Setup(x => x.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan?>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
                              .ReturnsAsync(true);

            // Act
            await _cacheService.AddOrUpdateAsync(key, value);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    null,
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_CacheFailed()
        {
            // Arrange
            var key = "TestKey";
            var value = "TestValue";

            _redisDatabaseMock.Setup(x => x.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan?>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
                              .ReturnsAsync(false);

            // Act
            await _cacheService.AddOrUpdateAsync(key, value);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    null,
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_CacheHit()
        {
            // Arrange
            var key = "TestKey";
            var expectedValue = "TestValue";
            var serializedValue = "\"TestValue\"";

            _redisDatabaseMock.Setup(x => x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                              .ReturnsAsync(serializedValue);
            _jsonSerializerMock.Setup(x => x.Deserialize<string>(serializedValue))
                               .Returns(expectedValue);

            // Act
            var result = await _cacheService.GetAsync<string>(key);

            // Assert
            result.Should().Be(expectedValue);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    null,
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_CacheMiss()
        {
            // Arrange
            var key = "NonExistingKey";

            _redisDatabaseMock.Setup(x => x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                              .ReturnsAsync(default(RedisValue));

            // Act
            var result = await _cacheService.GetAsync<string>(key);

            // Assert
            result.Should().BeNull();
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    null,
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}