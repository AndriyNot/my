using ExamProject.API.Data;
using ExamProject.API.Models.Dtos;
using ExamProject.API.Models.Requests;
using ExamProject.API.Models.Response;
using ExamProject.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedProductsResponse<Product>> GetPagedProductsAsync(PaginationProductsRequest<string> paginationRequest)
        {
            var query = _dbContext.Products
                .Include(p => p.Category) // Если используется отношение с категорией
                .AsQueryable();

            if (paginationRequest.Filters != null)
            {
                // Добавление фильтров к запросу, если они есть
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((paginationRequest.PageIndex - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginatedProductsResponse<Product>
            {
                PageIndex = paginationRequest.PageIndex,
                PageSize = paginationRequest.PageSize,
                Count = totalCount,
                Data = products
            };
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                // Логика обработки отсутствия продукта, например, выброс исключения или возврат другого значения по умолчанию
                throw new InvalidOperationException($"Product with id {id} not found.");
            }

            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
