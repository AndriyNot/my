using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogBrandRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(CatalogBrand catalogBrand)
        {
            await _dbContext.CatalogBrands.AddAsync(catalogBrand);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _dbContext.CatalogBrands.FindAsync(id);
            if (brand == null)
            {
                return false;
            }

            _dbContext.CatalogBrands.Remove(brand);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CatalogBrand catalogBrand)
        {
            _dbContext.CatalogBrands.Update(catalogBrand);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
