using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item1 = new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        };
        var item = await _dbContext.AddAsync(item1);

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        _dbContext.CatalogItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(CatalogItem catalogItem)
    {
        var existingItem = await _dbContext.CatalogItems.FindAsync(catalogItem.Id);
        if (existingItem == null)
        {
            return false;
        }

        existingItem.Name = catalogItem.Name;
        existingItem.Description = catalogItem.Description;
        existingItem.Price = catalogItem.Price;
        existingItem.AvailableStock = catalogItem.AvailableStock;
        existingItem.CatalogBrandId = catalogItem.CatalogBrandId;
        existingItem.CatalogTypeId = catalogItem.CatalogTypeId;
        existingItem.PictureFileName = catalogItem.PictureFileName;

        _dbContext.CatalogItems.Update(existingItem);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}