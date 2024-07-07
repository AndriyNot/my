using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTests
    {
        private readonly ICatalogBrandService _catalogBrandService;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogBrand _testBrand = new CatalogBrand { Id = 1, Brand = "Test Brand" };
        private readonly CatalogBrandDto _testBrandDto = new CatalogBrandDto { Id = 1, Brand = "Test Brand" };

        public CatalogBrandServiceTests()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddCatalogBrandAsync_Success()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.AddAsync(It.IsAny<CatalogBrand>())).ReturnsAsync(true);
            _mapper.Setup(m => m.Map<CatalogBrand>(It.IsAny<CatalogBrandDto>())).Returns(_testBrand);

            // Act
            var result = await _catalogBrandService.AddCatalogBrandAsync(_testBrandDto);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddCatalogBrandAsync_Failed()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.AddAsync(It.IsAny<CatalogBrand>())).ReturnsAsync(false);
            _mapper.Setup(m => m.Map<CatalogBrand>(It.IsAny<CatalogBrandDto>())).Returns(_testBrand);

            // Act
            var result = await _catalogBrandService.AddCatalogBrandAsync(_testBrandDto);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCatalogBrandAsync_Success()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _catalogBrandService.DeleteCatalogBrandAsync(_testBrand.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteCatalogBrandAsync_Failed()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _catalogBrandService.DeleteCatalogBrandAsync(_testBrand.Id);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateCatalogBrandAsync_Success()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.UpdateAsync(It.IsAny<CatalogBrand>())).ReturnsAsync(true);
            _mapper.Setup(m => m.Map<CatalogBrand>(It.IsAny<CatalogBrandDto>())).Returns(_testBrand);

            // Act
            var result = await _catalogBrandService.UpdateCatalogBrandAsync(_testBrandDto);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCatalogBrandAsync_Failed()
        {
            // Arrange
            _catalogBrandRepository.Setup(s => s.UpdateAsync(It.IsAny<CatalogBrand>())).ReturnsAsync(false);
            _mapper.Setup(m => m.Map<CatalogBrand>(It.IsAny<CatalogBrandDto>())).Returns(_testBrand);

            // Act
            var result = await _catalogBrandService.UpdateCatalogBrandAsync(_testBrandDto);

            // Assert
            result.Should().BeFalse();
        }
    }
}