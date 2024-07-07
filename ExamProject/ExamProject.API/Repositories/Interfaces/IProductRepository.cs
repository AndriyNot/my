using ExamProject.API.Models.Dtos;
using ExamProject.API.Models.Requests;
using ExamProject.API.Models.Response;

namespace ExamProject.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<PaginatedProductsResponse<Product>> GetPagedProductsAsync(PaginationProductsRequest<string> paginationRequest);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
