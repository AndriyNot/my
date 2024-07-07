using ExamProject.API.Data;
using ExamProject.API.Models.Dtos;
using ExamProject.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Repositories
{
    public class ProductDescriptionRepository : IProductDescriptionRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductDescriptionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductDescription>> GetProductDescriptionsAsync()
        {
            return await _dbContext.ProductDescriptions.ToListAsync();
        }

        public async Task<ProductDescription> GetProductDescriptionByIdAsync(int id)
        {
            var productDescription = await _dbContext.ProductDescriptions.FindAsync(id);

            if (productDescription == null)
            {
                // Логика обработки отсутствия описания продукта, например, выброс исключения или возврат другого значения по умолчанию
                throw new InvalidOperationException($"Product description with id {id} not found.");
            }

            return productDescription;
        }

        public async Task<ProductDescription> CreateProductDescriptionAsync(ProductDescription productDescription)
        {
            _dbContext.ProductDescriptions.Add(productDescription);
            await _dbContext.SaveChangesAsync();
            return productDescription;
        }

        public async Task UpdateProductDescriptionAsync(ProductDescription productDescription)
        {
            _dbContext.Entry(productDescription).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductDescriptionAsync(int id)
        {
            var productDescription = await _dbContext.ProductDescriptions.FindAsync(id);
            if (productDescription != null)
            {
                _dbContext.ProductDescriptions.Remove(productDescription);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
