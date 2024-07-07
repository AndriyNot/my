using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(CatalogType catalogType)
        {
            await _dbContext.CatalogTypes.AddAsync(catalogType);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _dbContext.CatalogTypes.FindAsync(id);
            if (type == null)
            {
                return false;
            }

            _dbContext.CatalogTypes.Remove(type);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CatalogType catalogType)
        {
            _dbContext.CatalogTypes.Update(catalogType);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
