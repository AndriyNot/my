using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<bool> AddCatalogTypeAsync(CatalogTypeDto catalogTypeDto);
        Task<bool> DeleteCatalogTypeAsync(int id);
        Task<bool> UpdateCatalogTypeAsync(CatalogTypeDto catalogTypeDto);
    }
}
