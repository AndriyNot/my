using Moq;
using AutoMapper;
using Catalog.Host.Services;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure.Services;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTests
    {
        private readonly CatalogTypeDto _testCatalogTypeDto = new CatalogTypeDto()
        {
            Type = "TestType"
        };

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogTypeService _catalogTypeService;

        public CatalogTypeServiceTests()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddCatalogTypeAsync_Success()
        {
            // Arrange
            bool expectedResult = true;
            _mapper.Setup(m => m.Map<CatalogType>(_testCatalogTypeDto)).Returns(new CatalogType());
            _catalogTypeRepository.Setup(c => c.AddAsync(It.IsAny<CatalogType>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.AddCatalogTypeAsync(_testCatalogTypeDto);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task AddCatalogTypeAsync_Failed()
        {
            // Arrange
            bool expectedResult = false;
            _mapper.Setup(m => m.Map<CatalogType>(_testCatalogTypeDto)).Returns(new CatalogType());
            _catalogTypeRepository.Setup(c => c.AddAsync(It.IsAny<CatalogType>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.AddCatalogTypeAsync(_testCatalogTypeDto);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task DeleteCatalogTypeAsync_Success()
        {
            // Arrange
            int typeIdToDelete = 1;
            bool expectedResult = true;
            _catalogTypeRepository.Setup(c => c.DeleteAsync(typeIdToDelete)).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.DeleteCatalogTypeAsync(typeIdToDelete);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task DeleteCatalogTypeAsync_Failed()
        {
            // Arrange
            int typeIdToDelete = 1;
            bool expectedResult = false;
            _catalogTypeRepository.Setup(c => c.DeleteAsync(typeIdToDelete)).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.DeleteCatalogTypeAsync(typeIdToDelete);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task UpdateCatalogTypeAsync_Success()
        {
            // Arrange
            bool expectedResult = true;
            _mapper.Setup(m => m.Map<CatalogType>(_testCatalogTypeDto)).Returns(new CatalogType());
            _catalogTypeRepository.Setup(c => c.UpdateAsync(It.IsAny<CatalogType>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.UpdateCatalogTypeAsync(_testCatalogTypeDto);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task UpdateCatalogTypeAsync_Failed()
        {
            // Arrange
            bool expectedResult = false;
            _mapper.Setup(m => m.Map<CatalogType>(_testCatalogTypeDto)).Returns(new CatalogType());
            _catalogTypeRepository.Setup(c => c.UpdateAsync(It.IsAny<CatalogType>())).ReturnsAsync(expectedResult);

            // Act
            var result = await _catalogTypeService.UpdateCatalogTypeAsync(_testCatalogTypeDto);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}