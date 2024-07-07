using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<bool> AddCatalogBrandAsync(CatalogBrandDto catalogBrandDto);
        Task<bool> DeleteCatalogBrandAsync(int id);
        Task<bool> UpdateCatalogBrandAsync(CatalogBrandDto catalogBrandDto);
    }
}
