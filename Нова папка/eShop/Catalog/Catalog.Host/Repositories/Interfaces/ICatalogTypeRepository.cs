using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<bool> AddAsync(CatalogType catalogType);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(CatalogType catalogType);
    }
}
