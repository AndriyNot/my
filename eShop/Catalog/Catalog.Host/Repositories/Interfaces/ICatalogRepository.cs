using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
        Task<CatalogItem> GetByIdAsync(int id);
        Task<IEnumerable<CatalogItem>> GetByBrandAsync(int brandId);
        Task<IEnumerable<CatalogItem>> GetByTypeAsync(int typeId);
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<IEnumerable<CatalogType>> GetTypesAsync();
    }
}
