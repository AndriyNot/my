using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogRepository catalogRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogRepository = catalogRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? brandFilter = null;
            int? typeFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Brand, out var brand))
                {
                    brandFilter = brand;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Type, out var type))
                {
                    typeFilter = type;
                }
            }

            var result = await _catalogRepository.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<CatalogItemDto> GetCatalogItemByIdAsync(int id)
    {
        var catalogItem = await _catalogRepository.GetByIdAsync(id);
        return _mapper.Map<CatalogItemDto>(catalogItem);
    }

    public async Task<IEnumerable<CatalogItemDto>> GetCatalogItemsByBrandAsync(int brandId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var items = await _catalogRepository.GetByBrandAsync(brandId);
            return items.Select(item => _mapper.Map<CatalogItemDto>(item)).ToList();
        });
    }

    public async Task<IEnumerable<CatalogItemDto>> GetCatalogItemsByTypeAsync(int typeId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var items = await _catalogRepository.GetByTypeAsync(typeId);
            return items.Select(item => _mapper.Map<CatalogItemDto>(item)).ToList();
        });
    }

    public async Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var brands = await _catalogRepository.GetBrandsAsync();
            return brands.Select(brand => _mapper.Map<CatalogBrandDto>(brand)).ToList();
        });
    }

    public async Task<IEnumerable<CatalogTypeDto>> GetTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var types = await _catalogRepository.GetTypesAsync();
            return types.Select(type => _mapper.Map<CatalogTypeDto>(type)).ToList();
        });
    }
}