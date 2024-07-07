using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems
                .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<CatalogItem> GetByIdAsync(int id)
        {
            var catalogItem = await _dbContext.CatalogItems.FindAsync(id);
            if (catalogItem == null)
            {
                throw new InvalidOperationException($"CatalogItem with id {id} not found.");
            }

            return catalogItem;
        }

        public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(int brandId)
        {
            return await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.CatalogBrandId == brandId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(int typeId)
        {
            return await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.CatalogTypeId == typeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            return await _dbContext.CatalogBrands.ToListAsync();
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            return await _dbContext.CatalogTypes.ToListAsync();
        }
    }
}
