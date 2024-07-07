using ExamProject.API.Models.Dtos;

namespace ExamProject.API.Services.Interfaces
{
    public interface IProductDescriptionService
    {
        Task<IEnumerable<ProductDescription>> GetProductDescriptionsAsync();
        Task<ProductDescription> GetProductDescriptionByIdAsync(int id);
        Task<ProductDescription> CreateProductDescriptionAsync(ProductDescription productDescription);
        Task UpdateProductDescriptionAsync(ProductDescription productDescription);
        Task DeleteProductDescriptionAsync(int id);
    }
}
