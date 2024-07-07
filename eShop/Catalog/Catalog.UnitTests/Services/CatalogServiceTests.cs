using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogRepository> _catalogRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogRepository = new Mock<ICatalogRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogRepository.Object, _mapper.Object);

        // Setup default mappings
        SetupDefaultMappings();
    }

    private void SetupDefaultMappings()
    {
        _mapper.Setup(m => m.Map<CatalogItemDto>(It.IsAny<CatalogItem>()))
            .Returns((CatalogItem src) => src == null ? null : new CatalogItemDto { Id = src.Id, Name = src.Name });

        _mapper.Setup(m => m.Map<List<CatalogItemDto>>(It.IsAny<List<CatalogItem>>()))
            .Returns((List<CatalogItem> src) => src?.Select(item => new CatalogItemDto { Id = item.Id, Name = item.Name }).ToList());

        _mapper.Setup(m => m.Map<List<CatalogBrandDto>>(It.IsAny<List<CatalogBrand>>()))
            .Returns((List<CatalogBrand> src) => src?.Select(brand => new CatalogBrandDto { Id = brand.Id, Brand = brand.Brand }).ToList());

        _mapper.Setup(m => m.Map<List<CatalogTypeDto>>(It.IsAny<List<CatalogType>>()))
            .Returns((List<CatalogType> src) => src?.Select(type => new CatalogTypeDto { Id = type.Id, Type = type.Type }).ToList());
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.IsAny<CatalogItem>())).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync((PaginatedItems<CatalogItem>)null);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Success()
    {
        // arrange
        var itemId = 1;
        var mockItem = new CatalogItem { Id = itemId, Name = "Item 1" };
        var mockItemDto = new CatalogItemDto { Id = itemId, Name = "Item 1" };

        _catalogRepository.Setup(repo => repo.GetByIdAsync(itemId)).ReturnsAsync(mockItem);
        _mapper.Setup(m => m.Map<CatalogItemDto>(mockItem)).Returns(mockItemDto);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(itemId);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(itemId);
        result.Name.Should().Be(mockItem.Name);
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Failed()
    {
        // arrange
        var itemId = 1;

        _catalogRepository.Setup(repo => repo.GetByIdAsync(itemId)).ReturnsAsync((CatalogItem)null);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(itemId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Success()
    {
        // arrange
        var brandId = 1;
        var mockItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };
        var mockItemsDto = new List<CatalogItemDto>
        {
            new CatalogItemDto { Id = 1, Name = "Item 1" },
            new CatalogItemDto { Id = 2, Name = "Item 2" }
        };

        _catalogRepository.Setup(repo => repo.GetByBrandAsync(brandId)).ReturnsAsync(mockItems);
        _mapper.Setup(m => m.Map<List<CatalogItemDto>>(mockItems)).Returns(mockItemsDto);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(brandId);

        // assert
        result.Should().NotBeNull();
        result.Count().Should().Be(mockItems.Count);
        foreach (var item in result)
        {
            item.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Failed()
    {
        // arrange
        var brandId = 1;

        _catalogRepository.Setup(repo => repo.GetByBrandAsync(brandId)).ReturnsAsync((List<CatalogItem>)null);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(brandId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Success()
    {
        // arrange
        var typeId = 1;
        var mockItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };
        var mockItemsDto = new List<CatalogItemDto>
        {
            new CatalogItemDto { Id = 1, Name = "Item 1" },
            new CatalogItemDto { Id = 2, Name = "Item 2" }
        };

        _catalogRepository.Setup(repo => repo.GetByTypeAsync(typeId)).ReturnsAsync(mockItems);
        _mapper.Setup(m => m.Map<List<CatalogItemDto>>(mockItems)).Returns(mockItemsDto);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(typeId);

        // assert
        result.Should().NotBeNull();
        result.Count().Should().Be(mockItems.Count);
        foreach (var item in result)
        {
            item.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Failed()
    {
        // arrange
        var typeId = 1;

        _catalogRepository.Setup(repo => repo.GetByTypeAsync(typeId)).ReturnsAsync((List<CatalogItem>)null);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(typeId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandsAsync_Success()
    {
        // arrange
        var mockBrands = new List<CatalogBrand>
            {
                new CatalogBrand { Id = 1, Brand = "Brand 1" },
                new CatalogBrand { Id = 2, Brand = "Brand 2" }
            };
        var mockBrandsDto = new List<CatalogBrandDto>
            {
                new CatalogBrandDto { Id = 1, Brand = "Brand 1" },
                new CatalogBrandDto { Id = 2, Brand = "Brand 2" }
            };

        _catalogRepository.Setup(repo => repo.GetBrandsAsync()).ReturnsAsync(mockBrands);

        // act
        var result = await _catalogService.GetBrandsAsync();

        // assert
        result.Should().NotBeNull();
        result.Count().Should().Be(mockBrands.Count);
        foreach (var brand in result)
        {
            brand.Should().NotBeNull();
            mockBrandsDto.Should().ContainEquivalentOf(brand);
        }
    }

    [Fact]
    public async Task GetBrandsAsync_Failed()
    {
        // arrange
        _catalogRepository.Setup(repo => repo.GetBrandsAsync()).ReturnsAsync((List<CatalogBrand>)null);

        // act
        var result = await _catalogService.GetBrandsAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTypesAsync_Success()
    {
        // arrange
        var mockTypes = new List<CatalogType>
        {
        new CatalogType { Id = 1, Type = "Type 1" },
        new CatalogType { Id = 2, Type = "Type 2" }
        };
        var mockTypesDto = new List<CatalogTypeDto>
        {
        new CatalogTypeDto { Id = 1, Type = "Type 1" },
        new CatalogTypeDto { Id = 2, Type = "Type 2" }
        };

        _catalogRepository.Setup(repo => repo.GetTypesAsync()).ReturnsAsync(mockTypes);
        _mapper.Setup(m => m.Map<List<CatalogTypeDto>>(mockTypes)).Returns(mockTypesDto);

        // act
        var result = await _catalogService.GetTypesAsync();

        // assert
        result.Should().NotBeNull();
        result.Count().Should().Be(mockTypes.Count);
        foreach (var type in result)
        {
            type.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetTypesAsync_Failed()
    {
        // arrange
        _catalogRepository.Setup(repo => repo.GetTypesAsync()).ReturnsAsync((List<CatalogType>)null);

        // act
        var result = await _catalogService.GetTypesAsync();

        // assert
        result.Should().BeNull();
    }
}