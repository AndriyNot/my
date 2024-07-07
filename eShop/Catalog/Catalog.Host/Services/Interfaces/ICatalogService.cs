using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<CatalogItemDto> GetCatalogItemByIdAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetCatalogItemsByBrandAsync(int brandId);
    Task<IEnumerable<CatalogItemDto>> GetCatalogItemsByTypeAsync(int typeId);
    Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
    Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
}