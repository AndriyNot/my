using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<bool> AddAsync(CatalogBrand catalogBrand);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(CatalogBrand catalogBrand);
    }
}
