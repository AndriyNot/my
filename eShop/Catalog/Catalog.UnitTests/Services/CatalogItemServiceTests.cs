using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Catalog.Host.Services;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.UnitTests.Services
{
    public class CatalogItemServiceTests
    {
        private readonly ICatalogItemService _catalogItemService;
        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogItemService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogItem _testItem = new CatalogItem()
        {
            Id = 1,
            Name = "Test Item",
            Description = "Test Description",
            Price = 100,
            AvailableStock = 10,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            PictureFileName = "test.jpg"
        };

        private readonly CatalogItemDto _testItemDto = new CatalogItemDto()
        {
            Name = "Name",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrand = new CatalogBrandDto { Id = 1, Brand = "Brand" },
            CatalogType = new CatalogTypeDto { Id = 1, Type = "Type" },
            PictureUrl = "1.png"
        };

        public CatalogItemServiceTests()
        {
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogItemService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogItemService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // Arrange
            int? testResult = 1;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // Act
            var result = await _catalogItemService.Add(
                _testItem.Name,
                _testItem.Description,
                _testItem.Price,
                _testItem.AvailableStock,
                _testItem.CatalogBrandId,
                _testItem.CatalogTypeId,
                _testItem.PictureFileName);

            // Assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // Arrange
            int? testResult = null;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // Act
            var result = await _catalogItemService.Add(
                _testItem.Name,
                _testItem.Description,
                _testItem.Price,
                _testItem.AvailableStock,
                _testItem.CatalogBrandId,
                _testItem.CatalogTypeId,
                _testItem.PictureFileName);

            // Assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateCatalogItemAsync_Success()
        {
            // Arrange
            _catalogItemRepository.Setup(s => s.UpdateAsync(It.IsAny<CatalogItem>())).ReturnsAsync(true);
            _mapper.Setup(m => m.Map<CatalogItem>(It.IsAny<CatalogItemDto>())).Returns(_testItem);

            // Act
            var result = await _catalogItemService.UpdateCatalogItemAsync(_testItemDto);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCatalogItemAsync_Failed()
        {
            // Arrange
            _catalogItemRepository.Setup(s => s.UpdateAsync(It.IsAny<CatalogItem>())).ReturnsAsync(false);
            _mapper.Setup(m => m.Map<CatalogItem>(It.IsAny<CatalogItemDto>())).Returns(_testItem);

            // Act
            var result = await _catalogItemService.UpdateCatalogItemAsync(_testItemDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCatalogItemAsync_Success()
        {
            // Arrange
            _catalogItemRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _catalogItemService.DeleteCatalogItemAsync(_testItem.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteCatalogItemAsync_Failed()
        {
            // Arrange
            _catalogItemRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _catalogItemService.DeleteCatalogItemAsync(_testItem.Id);

            // Assert
            result.Should().BeFalse();
        }
    }
}
